// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Diagnostics.Metrics;
using System.IO;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Identitys;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
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

        private KaoListDataContext CreateScopedDataContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
        }

        //[Authorize(Roles = KaoListRole.Administrator)]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongResource), StatusCodes.Status200OK)]
        //[ValidateAntiForgeryToken]
        public IActionResult Post(
            [FromQuery(Name = "part")] string[] parts,
            [FromQuery(Name = "type")] string type,
            SongResource resource
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (type == "inst")
                {
                    CreateInstAndSing(parts, resource);
                }
                else if (type == "sing")
                {
                    CreateSing(parts, resource);
                }
                else
                {
                    return BadRequest("Invalid type specified.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: 로그 기록
                return StatusCode(500, "Internal server error");
            }
        }

        private void CreateInstAndSing(string[] parts, SongResource resource)
        {
            var instrumental = new Instrumental
            {
                Created = DateTime.UtcNow,
            };

            if (parts.Contains("snippet"))
            {
                if (resource.Snippet == null || string.IsNullOrEmpty(resource.Snippet.Title))
                {
                    throw new ArgumentException("Snippet or Title is missing in the resource.");
                }

                instrumental.Title = resource.Snippet.Title;
                instrumental.NormalizedTitle = resource.Snippet.Title.ToUpper();
                instrumental.Composer = resource.Snippet.Composer;
                instrumental.SoundId = resource.Snippet.Thumbnail?.Url;
            }

            _context.Instrumental.Add(instrumental);

            if (parts.Contains("id") || parts.Contains("snippet"))
            {
                var sing = new Sing
                {
                    InstrumentalId = instrumental.Id,
                };

                if (parts.Contains("snippet"))
                {
                    sing.SoundId = instrumental.SoundId;
                }

                _context.Sings.Add(sing);
            }

            _context.SaveChanges();
        }

        private void CreateSing(string[] parts, SongResource resource)
        {
            if (resource.Id.IsNullOrEmpty() == true)
            {
                throw new ArgumentNullException(nameof(resource.Id), "Id is not provided");
            }

            var instrumental = _context.Instrumental.Where(i => i.Id == resource.Id).FirstOrDefault();

            if (instrumental == null)
            {
                throw new InvalidOperationException($"No Instrumental found with the Id {resource.Id}");
            }

            if (parts.Contains("id") || parts.Contains("snippet"))
            {
                var sing = new Sing
                {
                    InstrumentalId = resource.Id,
                };

                if (parts.Contains("snippet"))
                {
                    sing.SoundId = instrumental.SoundId;
                }

                _context.Sings.Add(sing);
            }

            _context.SaveChanges();
        }


        //[Authorize(Roles = KaoListRoles.Administrator)]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongResource), StatusCodes.Status200OK)]
        //[ValidateAntiForgeryToken]
        public IActionResult Put(
            [FromQuery(Name = "part")] string[] parts,
            [FromQuery(Name = "type")] string type,
            SongResource resource
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (type == "inst")
                {
                    UpdateInst(parts, resource);
                }

                if (type == "sing")
                {
                    UpdateSing(parts, resource);
                }

                else
                {
                    return BadRequest("Invalid type specified.");
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private void UpdateInst(string[] parts, SongResource resource)
        {
            if (resource.Id.IsNullOrEmpty() == true)
            {
                throw new ArgumentNullException(nameof(resource.Id), "Id is not provided");
            }

            var instrumental = _context.Instrumental.Where(i => i.Id == resource.Id).FirstOrDefault();

            if (instrumental == null)
            {
                throw new InvalidOperationException($"No Instrumental found with the Id {resource.Id}");
            }

            if (parts.Contains("snippet"))
            {
                if (resource.Snippet == null || string.IsNullOrEmpty(resource.Snippet.Title))
                {
                    throw new ArgumentException("Snippet or Title is missing in the resource.");
                }

                instrumental.Title = resource.Snippet.Title;
                instrumental.NormalizedTitle = resource.Snippet.Title.ToUpper();
                instrumental.Composer = resource.Snippet.Composer;
                instrumental.SoundId = resource.Snippet.Thumbnail?.Url;
            }

            _context.SaveChanges();
        }

        private void UpdateSing(string[] parts, SongResource resource)
        {
            if (resource.Id.IsNullOrEmpty() == true)
            {
                throw new ArgumentNullException(nameof(resource.Id), "Id is not provided");
            }

            var sing = _context.Sings.Where(i => i.Id == resource.Id).FirstOrDefault();

            if (sing == null)
            {
                throw new InvalidOperationException($"No Sing found with the Id {resource.Id}");
            }

            if (parts.Contains("snippet"))
            {
                if (resource.Snippet == null || string.IsNullOrEmpty(resource.Snippet.Title))
                {
                    throw new ArgumentException("Snippet or Title is missing in the resource.");
                }

                sing.SoundId = resource.Snippet.Thumbnail?.Url;
            }

            _context.SaveChanges();
        }

        //[Authorize(Roles = KaoListRoles.Administrator)]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongResource), StatusCodes.Status200OK)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(
            [FromQuery(Name = "type")] string type,
            string id
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (type == "inst")
                {
                    await DeleteInstAsync(id);
                }

                if (type == "sing")
                {
                    await DeleteSingAsync(id);
                }

                else
                {
                    return BadRequest("Invalid type specified.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task DeleteInstAsync(string id)
        {
            await DeleteEntityByIdAsync<Instrumental>(id);
        }

        private async Task DeleteSingAsync(string id)
        {
            await DeleteEntityByIdAsync<Sing>(id);
        }

        private async Task DeleteEntityByIdAsync<TEntity>(string id) where TEntity : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Id is not provided");
            }

            var context = CreateScopedDataContext();
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} found with the Id {id}");
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
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
            var context = CreateScopedDataContext();

            if (ids == null || !ids.Any())
            {
                return 0;
            }
            else
            {
                return await context.Sings.Where(sing => ids.Contains(sing.Id)).CountAsync();
            }
        }

        [HttpPost("rate")]
        [ProducesResponseType(typeof(SongListResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> setRating(string songId, SongRating rating, string userId)
        {
            var context = CreateScopedDataContext();

            var songExists = await _context.Sings.AnyAsync(s => s.Id == songId);
            if (!songExists)
            {
                return NotFound(new { error = "Song not found." });
            }

            switch (rating)
            {
                case SongRating.Follow:
                    var follow = new SingFollower
                    {
                        SingId = songId,
                        UserId = userId,
                        Created = DateTime.Now
                    };
                    context.SingFollowers.Add(follow);
                    break;

                case SongRating.Blind:
                    var blind = new SingBlind
                    {
                        SingId = songId,
                        UserId = userId,
                        Created = DateTime.Now
                    };
                    context.SingBlinds.Add(blind);
                    break;

                case SongRating.None:
                    break;
            }

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongListResponse), StatusCodes.Status200OK)]
        public async Task<SongListResponse> GetListAsync(
            [FromQuery(Name = "part")] SongPart[] parts,
            [FromQuery(Name = "id")] string[]? ids,
            string? myRating,
            int offset = 0,
            int maxResults = 20
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
