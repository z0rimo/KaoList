// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Search;
using CodeRabbits.KaoList.Web.Models.Searchs;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using CodeRabbits.KaoList.Web.Services;
using CodeRabbits.KaoList.Web.Services.YouTubes;
using Duende.IdentityServer.Extensions;
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

        private async Task<int> CalculateTotalResultsAsync(string query)
        {
            var context = CreateScopedDataContext();
            var totalResults = 0;

            totalResults += await context.Instrumental.CountAsync(inst => inst.NormalizedTitle!.Equals(query));

            totalResults += await context.Instrumental.CountAsync(inst => inst.NormalizedTitle!.Contains(query) && !inst.NormalizedTitle.Equals(query));

            totalResults += await context.SingUsers
                                         .Join(context.Users, su => su.UserId, user => user.Id, (su, user) => new { su, user })
                                         .CountAsync(su => su.user.NormalizedNickName == query);

            return totalResults;
        }

        private async Task<IEnumerable<Instrumental>> SearchInstrumentalsAsync(string query, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();

            var exactTitleMatchesTask = context.Instrumental
                                               .Where(inst => inst.NormalizedTitle!.Equals(query))
                                               .OrderByDescending(inst => inst.Created);

            var titleContainsMatchesTask = context.Instrumental
                                                  .Where(inst => inst.NormalizedTitle!.Contains(query) && !inst.NormalizedTitle.Equals(query))
                                                  .OrderByDescending(inst => inst.Created);

            var singUserExactMatchesTask = SearchSingUserExactMatchesAsync(query);

            var exactTitleMatches = await exactTitleMatchesTask.ToListAsync();
            var titleContainsMatches = await titleContainsMatchesTask.ToListAsync();
            var singUserExactMatches = await singUserExactMatchesTask;

            var combinedResults = exactTitleMatches.Concat(titleContainsMatches)
                                                   .Concat(singUserExactMatches)
                                                   .Distinct()
                                                   .Skip(offset)
                                                   .Take(maxResults);

            return combinedResults;
        }


        private async Task<IEnumerable<Instrumental>> SearchSingUserExactMatchesAsync(string query)
        {
            var context = CreateScopedDataContext();
            var userMatches = context.Users
                                     .Where(user => user.NickName == query)
                                     .Select(user => user.Id);

            var singMatches = context.SingUsers
                                     .Where(su => userMatches.Contains(su.UserId))
                                     .Select(su => su.SingId);

            var instrumentalMatches = context.Sings
                                             .Where(sing => singMatches.Contains(sing.Id))
                                             .Select(sing => sing.InstrumentalId);

            var instrumentals = await context.Instrumental
                                             .Where(inst => instrumentalMatches.Contains(inst.Id))
                                             .OrderByDescending(inst => inst.Created)
                                             .ToListAsync();

            return instrumentals;
        }


        private async Task<IEnumerable<SearchResource>> GetSearchItemByIdAsync(IEnumerable<string> querys, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();
            var allQueries = querys.SelectMany(q => q.Split(',')).ToList();

            /*var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where allQueries.Contains(inst.NormalizedTitle!)
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst
                               })
                               .OrderBy(s => s.Instrumental.Id)
                               .Skip(offset)
                               .Take(maxResults)
                               .ToListAsync();*/

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

        private async Task<IEnumerable<SearchResource>> GetSearchItemBySnippetAsync(IEnumerable<string> queries, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();
            var token = HttpContext.GetIdentityToken();
            var userId = _userManager.GetUserId(User);
            var searchResults = new List<SearchResource>();

            foreach (var query in queries)
            {
                var instrumentals = await SearchInstrumentalsAsync(query, offset, maxResults);

                foreach (var inst in instrumentals)
                {
                    var sings = await context.Sings
                        .Where(s => s.InstrumentalId == inst.Id)
                        .ToListAsync();

                    foreach (var sing in sings)
                    {
                        var singUsers = await context.SingUsers
                            .Where(su => su.SingId == sing.Id)
                            .Join(context.Users, su => su.UserId, user => user.Id, (su, user) => new { su, user.NickName })
                            .ToListAsync();

                        var karaokeInfo = await context.Karaokes
                            .Where(k => k.SingId == sing.Id)
                            .Select(k => new { k.Provider, k.No })
                            .FirstOrDefaultAsync();

                        await _logService.CreateSongSearchLogAsync(query, sing.Id, userId, token);

                        var resource = new SearchResource
                        {
                            Id = new SearchSong
                            {
                                Id = sing.Id
                            },
                            Etag = inst.ConcurrencyStamp,
                            Snippet = new SongSnippet
                            {
                                Created = sing.Created,
                                Title = inst.Title,
                                SongUsers = singUsers.Select(su => new SongUser
                                {
                                    Id = su.su.UserId,
                                    Nickname = su.NickName
                                }).ToList(),
                                Composer = inst.Composer,
                                Thumbnail = inst.SoundId == null ? null : new ThumbnailResource
                                {
                                    Url = inst.SoundId,
                                    Width = 300,
                                    Height = 300
                                },
                                Karaoke = karaokeInfo == null ? null : new SongKaraokeItem
                                {
                                    No = karaokeInfo.No,
                                    ProviderName = karaokeInfo.Provider
                                }
                            }
                        };

                        searchResults.Add(resource);
                    }
                }
            }

            return searchResults.Distinct().ToList();
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
                            items.AddRange(searchItemsBySnippet);
                            break;
                    }
                }
            }

            var totalResults = querys != null ? await CalculateTotalResultsAsync(querys.FirstOrDefault()) : 0;
            var resultsPerPage = items.Count;
            var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

            return new SearchListResponse
            {
                Etag = new Guid().ToString(),
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
