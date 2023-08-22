// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Security.Claims;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Search;
using CodeRabbits.KaoList.Web.Models.Searchs;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
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

        public SearchController(
            KaoListDataContext context,
            IServiceScopeFactory serviceScopeFactory
            )
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        private KaoListDataContext CreateScopedDataContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        }

        private async Task<int> GetTotalResultsFromDBAsync(IEnumerable<string>? querys)
        {
            var context = CreateScopedDataContext();

            if (querys == null || !querys.Any())
            {
                return 0;
            }
            else
            {
                var queryList = querys.ToList();
                return await context.Instrumental.Where(inst => queryList.Contains(inst.NormalizedTitle)).CountAsync();
            }
        }


        private async Task<IEnumerable<SearchResource>> GetSearchItemByIdAsync(IEnumerable<string> querys, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();
            var allQueries = querys.SelectMany(q => q.Split(',')).ToList();

            var songs = await (from sing in context.Sings
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
                               .ToListAsync();

            return songs.Select(song => new SearchResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = new SearchSongId
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
                Id = new SearchSongId
                {
                    Id = song.Sing.Id
                },
                Etag = song.Instrumental.ConcurrencyStamp,
                Snippet = new SearchSnippet
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
            int offset = 0,
            int maxResults = 20
            )
        {
            if (parts == null || !parts.Any())
            {
                parts = new[] { SearchPart.Snippet };
            }

            var items = new List<SearchResource>();
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (querys != null && querys.Any())
            {
                var log = new SongSearchLog
                {
                    Id = Guid.NewGuid().ToString(),
                    Query = string.Join(",", querys),
                    UserId = "8ab65dd9-bce0-4c1e-b3f1-4e2b613e444e",
                    IdentityToken = token
                };

                _context.SongSearchLogs.Add(log);
                await _context.SaveChangesAsync();

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

            int totalResults = await GetTotalResultsFromDBAsync(querys);
            int resultsPerPage = items.Count;
            int nextOffset = offset + maxResults;
            int prevOffset = offset - maxResults > 0 ? offset - maxResults : 0;

            return new SearchListResponse
            {
                Etag = new Guid().ToString(),
                Items = items,
                NextPageToken = nextOffset < totalResults ? nextOffset : null,
                PrevPageToken = offset > 0 ? prevOffset : null,
                PageInfo = new PageInfo
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }
    }
}
