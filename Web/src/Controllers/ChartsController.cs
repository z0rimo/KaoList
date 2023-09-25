// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;
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
    public class ChartsController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ChartsController(KaoListDataContext context, IServiceScopeFactory serviceScopeFactory)
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
            var initQuery = context.Sings.AsQueryable();

            if (date.HasValue)
            {
                initQuery = initQuery.Where(s => s.Created == date.Value.Date);
            }

            return await initQuery.CountAsync();
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
                Snippet = new ChartSnippet
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

        [HttpGet("list/discover")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DiscoverChartListResponse), StatusCodes.Status200OK)]
        public async Task<DiscoverChartListResponse> GetDiscoverChartListAsync(
            [FromQuery(Name = "part")] ChartPart part,
            [FromQuery] DateTime? date,
            [FromQuery(Name = "page")] int page = 1,
            int maxResults = 20
            )
        {
            int offset = (page - 1) * maxResults;
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
            var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

            return new DiscoverChartListResponse
            {
                Etag = Guid.NewGuid().ToString(),
                Resources = resources,
                NextPageToken = nextPageToken,
                PrevPageToken = prevPageToken,
                PageInfo = new PageInfo()
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }

        private async Task<int> GetSongFollowCountAsync(string singId, DateTime? startDate, DateTime? endDate)
        {
            var context = CreateScopedDataContext();

            return await context.SingFollowers
                .Where(f => f.SingId == singId && f.Created >= startDate && f.Created <= endDate)
                .CountAsync();
        }

        private async Task<int> GetSongSearchCountAsync(string query, DateTime? startDate, DateTime? endDate)
        {
            var context = CreateScopedDataContext();

            return await context.SongSearchLogs
                .Where(s => s.Query!.Contains(query) && s.Created >= startDate && s.Created <= endDate)
                .CountAsync();
        }

        private async Task<double> CalculateSongScoreAsync(string singId, DateTime? startDate, DateTime? endDate)
        {
            var context = CreateScopedDataContext();

            //var title = context.Sings.Where(s => s.Id);
            //TODO: search title 로그 부분 처리

            double followWeight = 0.5;
            double searchWeight = 0.1;

            int followCount = await GetSongFollowCountAsync(singId, startDate, endDate);
            int searchCount = await GetSongSearchCountAsync(singId, startDate, endDate);

            return (followCount * followWeight) + (searchCount * searchWeight);
        }

        private async Task<List<PopularSing>> GetRankedSongsAsync(DateTime? startDate, DateTime? endDate)
        {
            var context = CreateScopedDataContext();
            var sings = await context.Sings.ToListAsync();

            var songScores = new List<PopularSing>();

            foreach (var sing in sings)
            {
                var score = await CalculateSongScoreAsync(sing.Id!, startDate, endDate);
                songScores.Add(new PopularSing
                {
                    SingId = sing.Id,
                    Score = (float)score,
                    Created = DateTime.UtcNow
                });
            }

            return songScores.OrderByDescending(s => s.Score).ToList();
        }

        private async Task<IEnumerable<LikedChartResource>> GetDailyLikedSongsBySnippetAsync(int offset, int maxResults)
        {
            var context = CreateScopedDataContext();

            var songs = await (from ps in context.PopularDailySings
                               join sing in context.Sings on ps.SingId equals sing.Id
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               orderby ps.Created descending, inst.Title
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst,
                                   PopularDailySing = ps,
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

            int rank = offset + 1;

            return songs.Select(song => new LikedChartResource
            {
                Id = song.Sing.Id,
                Snippet = new LikedChartSnippet
                {
                    Rank = rank++,
                    Created = song.PopularDailySing.Created,
                    Title = song.Instrumental.Title,
                    SongUsers = song.SongUsers.Select(su => new SongUser
                    {
                        Id = su.su.UserId,
                        Nickname = su.NickName
                    }),
                    Composer = song.Instrumental.Composer,
                    Thumbnail = new ThumbnailResource
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

        [HttpGet("list/like")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LikedChartListResponse), StatusCodes.Status200OK)]
        public async Task<LikedChartListResponse> GetLikedChartListAsync(
            [FromQuery(Name = "part")] ChartPart part,
            [FromQuery(Name = "page")] int page = 1,
            int maxResults = 20
            )
        {
            int offset = (page - 1) * maxResults;
            var resources = new List<LikedChartResource>();

            if (part.Equals(ChartPart.Id))
            {
            }
            else
            {
                var dailyLikedChartItemsBySnippet = await GetDailyLikedSongsBySnippetAsync(offset, maxResults);
                resources.AddRange(dailyLikedChartItemsBySnippet);
            }


            int totalResults = 100;
            int resultsPerPage = resources.Count;
            var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

            return new LikedChartListResponse
            {
                Etag = Guid.NewGuid().ToString(),
                Resources = resources,
                NextPageToken = nextPageToken,
                PrevPageToken = prevPageToken,
                PageInfo = new PageInfo()
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }

        private async Task<IEnumerable<LikedChartResource>> GetTotalLikedSongsBySnippetAsync(int offset, int maxResults)
        {
            var context = CreateScopedDataContext();
            var songs = await (from ps in context.PopularSings
                               join sing in context.Sings on ps.SingId equals sing.Id
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               orderby ps.Created descending, inst.Title
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst,
                                   PopularSing = ps,
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

            int rank = offset + 1;

            return songs.Select(song => new LikedChartResource
            {
                Id = song.Sing.Id,
                Snippet = new LikedChartSnippet
                {
                    Rank = rank++,
                    Created = song.PopularSing.Created,
                    Title = song.Instrumental.Title,
                    SongUsers = song.SongUsers.Select(su => new SongUser
                    {
                        Id = su.su.UserId,
                        Nickname = su.NickName
                    }),
                    Composer = song.Instrumental.Composer,
                    Thumbnail = new ThumbnailResource
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

        [HttpGet("list/totalLike")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LikedChartListResponse), StatusCodes.Status200OK)]
        public async Task<LikedChartListResponse> GetTotalLikedChartListAsync(
            [FromQuery(Name = "part")] ChartPart part,
            [FromQuery(Name = "page")] int page = 1,
            int maxResults = 20
            )
        {
            int offset = (page - 1) * maxResults;
            var resources = new List<LikedChartResource>();

            if (part.Equals(ChartPart.Id))
            {

            }
            else
            {
                var totalLikedChartItemsBySnippet = await GetTotalLikedSongsBySnippetAsync(offset, maxResults);
                resources.AddRange(totalLikedChartItemsBySnippet);
            }

            int totalResults = 100;
            int resultsPerPage = resources.Count;
            var (nextPageToken, prevPageToken) = PaginationHelper.CalculatePageTokens(offset, maxResults, totalResults);

            return new LikedChartListResponse
            {
                Etag = Guid.NewGuid().ToString(),
                Resources = resources,
                NextPageToken = nextPageToken,
                PrevPageToken = prevPageToken,
                PageInfo = new PageInfo()
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }
    }
}
