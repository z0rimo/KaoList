// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Web.Identitys;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SongsController(KaoListDataContext context, IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [Authorize(Roles = KaoListRoles.Administrator)]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongResource), StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public IActionResult Post(
            [FromQuery(Name = "part")] SongPart[] parts,
            SongResource resource
            )
        {
            if (resource == null)
            {
                return BadRequest("SongResource is can't be null");
            }

            if (parts.Contains(SongPart.Id))
            {
                var existingSong = _context.Sings.Find(resource.Id);
                if (existingSong != null)
                {
                    _context.Sings.Remove(existingSong);
                }
            }
            else
            {
                // add this
            }

            _context.SaveChanges();
            return Ok(resource);
        }

        [Authorize(Roles = KaoListRoles.Administrator)]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongResource), StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public IActionResult Put(
            [FromQuery(Name = "part")] SongPart[] parts,
            SongResource resource
            )
        {
            throw new NotImplementedException();
        }

        private KaoListDataContext CreateScopedDataContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        }

        [Authorize(Roles = KaoListRoles.Administrator)]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongResource), StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(

            )
        {
            throw new NotImplementedException();
        }


        private async Task<IEnumerable<SongResource>> GetSongItemsByIdAsync(IEnumerable<string> ids, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();

            var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where ids.Contains(sing.Id)
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst
                               })
                               .OrderBy(s => s.Sing.Id)
                               .Skip(offset)
                               .Take(maxResults)
                               .ToListAsync();

            return songs.Select(song => new SongResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = song.Sing.Id
            }).ToList();
        }

        private async Task<IEnumerable<SongResource>> GetSongItemsBySnippetAsync(IEnumerable<string> ids, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();

            var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where ids.Contains(sing.Id)
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
                               .OrderBy(s => s.Sing.Id)
                               .Skip(offset)
                               .Take(maxResults)
                               .ToListAsync();

            return songs.Select(song => new SongResource
            {
                Id = song.Sing.Id,
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

        private async Task<IEnumerable<SongResource>> GetSongItemsByStatsticsAsync(IEnumerable<string> ids, int offset, int maxResults)
        {
            var context = CreateScopedDataContext();

            var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where ids.Contains(sing.Id)
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst,
                                   FollowCount = context.SingFollowers.Count(f => f.SingId == sing.Id),
                                   BlindCount = context.SingBlinds.Count(b => b.SingId == sing.Id)
                               })
                               .OrderBy(s => s.Sing.Id)
                               .Skip(offset)
                               .Take(maxResults)
                               .ToListAsync();

            return songs.Select(song => new SongResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = song.Sing.Id,
                Statistics = new SongStatistics
                {
                    FollowCount = (uint)song.FollowCount,
                    BlindCount = (uint)song.BlindCount
                }
            }).ToList();
        }

        private async Task<int> GetTotalResultsFromDBAsync(IEnumerable<string>? ids)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<KaoListDataContext>();

            if (ids == null || !ids.Any())
            {
                return 0;
            }
            else
            {
                return await context.Sings.Where(sing => ids.Contains(sing.Id)).CountAsync();
            }
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongListResponse), StatusCodes.Status200OK)]
        
        public async Task<SongListResponse> GetListAsync(
            [FromQuery(Name = "part")] SongPart[] parts,
            [FromQuery(Name = "id")] string[]? ids,
            [FromQuery] string? myRating,
            [FromQuery] int offset = 0,
            [FromQuery] int maxResults = 20
            )
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }

            var items = new List<SongResource>();

            if (ids != null && ids.Any())
            {
                foreach (var part in parts)
                {
                    switch (part)
                    {
                        case SongPart.Id:
                            var songItemsById = await GetSongItemsByIdAsync(ids, offset, maxResults);
                            items.AddRange(songItemsById);
                            break;

                        case SongPart.Snippet:
                            var songItemsBySnippet = await GetSongItemsBySnippetAsync(ids, offset, maxResults);
                            items.AddRange(songItemsBySnippet);
                            break;

                        case SongPart.Statistics:
                            var songItemsByStatstics = await GetSongItemsByStatsticsAsync(ids, offset, maxResults);
                            items.AddRange(songItemsByStatstics);
                            break;
                    }
                }
            }

            int totalResults = await GetTotalResultsFromDBAsync(ids);
            int resultsPerPage = items.Count;
            int nextOffset = offset + maxResults;
            int prevOffset = offset - maxResults > 0 ? offset - maxResults : 0;

            return new SongListResponse
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
