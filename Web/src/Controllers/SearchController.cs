// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Search;
using CodeRabbits.KaoList.Web.Models.Searchs;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using CodeRabbits.KaoList.Web.Services;
using CodeRabbits.KaoList.Web.Services.YouTubes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly UserManager<KaoListUser> _userManager;
        private readonly LogService _logService;
        private readonly SongService _songService;
        private readonly YouTubeService _youTubeService;

        public SearchController(
            KaoListDataContext context,
            IServiceScopeFactory serviceScopeFactory,
            UserManager<KaoListUser> userManager,
            LogService logService,
            SongService songService,
            YouTubeService youTubeService
            )
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
            _userManager = userManager;
            _logService = logService;
            _songService = songService;
            _youTubeService = youTubeService;
        }

        private KaoListDataContext CreateScopedDataContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        }

        private async Task<int> GetTotalResultsFromDBAsync(IEnumerable<string>? querys)
        {
            var context = CreateScopedDataContext();

            if (querys is null || !querys.Any())
            {
                return 0;
            }
            else
            {
                var queryList = querys.ToList();
                return await context.Instrumental.Where(inst => queryList.Contains(inst.NormalizedTitle!)).CountAsync();
            }
        }

        private async Task<IEnumerable<SearchResource>> GetSearchItemByIdAsync(IEnumerable<string> querys, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();
            var allQueries = querys.SelectMany(q => q.Split(',')).ToList();

            var songsQuery = from sing in context.Sings
                             join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                             select new { Sing = sing, Instrumental = inst };

            var songs = await songsQuery.ToListAsync();
            var filteredSongs = songs
                                .Where(s => allQueries.Any(q => s.Instrumental.NormalizedTitle!.Contains(q)))
                                .OrderBy(s => s.Instrumental.Id)
                                .Skip(offset)
                                .Take(maxResults)
                                .ToList();

            return filteredSongs.Select(song => new SearchResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = new SearchSong
                {
                    Id = song.Sing.Id
                }
            }).ToList();
        }

        private async Task<IEnumerable<SearchResource>> GetSearchItemBySnippetAsync(IEnumerable<string> querys, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();
            var allQueries = querys.SelectMany(q => q.Split(',')).ToList();

            var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where allQueries.Contains(inst.NormalizedTitle!)
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst,
                                   SongUsers = context.SingUsers
                                                      .Where(su => su.SingId == sing.Id)
                                                      .Join(context.Users,
                                                            su => su.UserId,
                                                            user => user.Id,
                                                            (su, user) => new { su, user.NickName })
                                                      .ToList(),
                                   KaraokeInfo = context.Karaokes
                                                        .Where(k => k.SingId == sing.Id)
                                                        .Select(k => new { k.Provider, k.No })
                                                        .FirstOrDefault()
                               })
                               .Skip(offset)
                               .Take(maxResults)
                               .ToListAsync();

            return songs.Select(song => new SearchResource
            {
                Id = new SearchSong
                {
                    Id = song.Sing.Id
                },
                Etag = song.Instrumental.ConcurrencyStamp,
                Snippet = new SongSnippet
                {
                    Created = song.Sing.Created,
                    Title = song.Instrumental.Title,
                    SongUsers = song.SongUsers.Select(su => new SongUser
                    {
                        Id = su.su.UserId,
                        Nickname = su.NickName
                    }),
                    Composer = song.Instrumental.Composer,
                    Thumbnail = song.Instrumental.SoundId == null ? null : new ThumbnailResource
                    {
                        Url = song.Instrumental.SoundId,
                        Width = 300,
                        Height = 300
                    },
                    Karaoke = new SongKaraokeItem
                    {
                        No = song.KaraokeInfo?.No,
                        ProviderName = song.KaraokeInfo?.Provider
                    }
                }
            }).ToList();
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SearchListResponse), StatusCodes.Status200OK)]
        public async Task<SearchListResponse> GetListAsync(
            [FromQuery(Name = "part")] SearchPart[] parts,
            [FromQuery(Name = "q")] string[]? querys,
            [FromQuery(Name = "page")] int page = 1,
            int maxResults = 20
            )
        {
            if (parts is null || !parts.Any())
            {
                parts = new[] { SearchPart.Snippet };
            }

            var offset = (page - 1) * maxResults;
            var items = new List<SearchResource>();
          
            if (querys is not null && querys.Any())
            {
                foreach (var part in parts)
                {
                    switch (part) 
                    {
                        case SearchPart.Id:
                            var searchItemsById = await GetSearchItemByIdAsync(querys, offset, maxResults);
                            items.AddRange(searchItemsById);
                            break;

                        case SearchPart.Snippet:
                            var searchItemsBySnippet = await GetSearchItemBySnippetAsync(querys, offset, maxResults);
                            await _songService.UpdateSoundIdUsingSearchSnippet(searchItemsBySnippet);
                            items.AddRange(searchItemsBySnippet);
                            break;
                    }
                }
            }

            var totalResults = await GetTotalResultsFromDBAsync(querys);
            var resultsPerPage = items.Count;
            var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

            return new SearchListResponse
            {
                Etag = Guid.NewGuid().ToString(),
                Items = items,
                NextPageToken = nextPageToken,
                PrevPageToken = prevPageToken,
                PageInfo = new PageInfo
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }
    }
}
