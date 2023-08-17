// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Charts;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ChartController(KaoListDataContext context, IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        private KaoListDataContext CreateScopedDataContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        }

        private async Task<int> GetTotalSongsByDate(DateTime? date = null)
        {
            var context = CreateScopedDataContext();
            var initialQuery = context.Sings.AsQueryable();

            if (date.HasValue)
            {
                initialQuery = initialQuery.Where(s => s.Created == date.Value.Date);
            }

            return await initialQuery.CountAsync();
        }

        private async Task<IEnumerable<DiscoverChartResource>> GetDiscoverChartItemsByIdAsync(int offset, int maxResults, DateTime? date = null)
        {
            var context = CreateScopedDataContext();

            var initialQuery = context.Sings.AsQueryable();
            if (date.HasValue)
            {
                initialQuery = initialQuery.Where(s => s.Created == date.Value.Date);
            }

            var songs = await (from sing in initialQuery
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               orderby sing.Created descending, inst.Title
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst
                               })
                               .Skip(offset)
                               .Take(maxResults)
                               .ToListAsync();

            return songs.Select(song => new DiscoverChartResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = song.Sing.Id
            }).ToList();
        }

        private async Task<IEnumerable<DiscoverChartResource>> GetDiscoverChartItemsBySnippetAsync(int offset, int maxResults, DateTime? date = null)
        {
            var context = CreateScopedDataContext();

            var initialQuery = context.Sings.AsQueryable();
            if (date.HasValue)
            {
                initialQuery = initialQuery.Where(s => s.Created == date.Value.Date);
            }

            var songs = await (from sing in initialQuery
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               orderby sing.Created descending, inst.Title
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

            return songs.Select(song => new DiscoverChartResource
            {
                Id = song.Sing.Id,
                Etag = song.Instrumental.ConcurrencyStamp,
                Snippet = new Models.Charts.ChartSnippet
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

        [HttpGet("discover")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DiscoverChartListResponse), StatusCodes.Status200OK)]
        public async Task<DiscoverChartListResponse> GetDiscoverChartListAsync(
    [FromQuery(Name = "part")] ChartPart part,
    [FromQuery] DateTime? date,
    string? myRating,
    int offset = 0,
    int maxResults = 20
    )
        {
            var resources = new List<DiscoverChartResource>();

            if (part.Equals(ChartPart.Id))
            {
                var discoverChartItemsById = await GetDiscoverChartItemsByIdAsync(offset, maxResults, date);
                resources.AddRange(discoverChartItemsById);
            }
            else
            {
                var discoverChartItemsBySnippet = await GetDiscoverChartItemsBySnippetAsync(offset, maxResults, date);
                resources.AddRange(discoverChartItemsBySnippet);
            }

            int totalResults = await GetTotalSongsByDate(date);
            int resultsPerPage = resources.Count;
            int nextOffset = offset + maxResults;
            int prevOffset = offset - maxResults > 0 ? offset - maxResults : 0;

            return new DiscoverChartListResponse
            {
                Etag = new Guid().ToString(),
                resources = resources,
                NextPageToken = nextOffset < totalResults ? nextOffset : null,
                PrevPageToken = offset > 0 ? prevOffset : null,
                PageInfo = new PageInfo()
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }
    }
}
