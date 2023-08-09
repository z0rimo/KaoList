// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web.Identitys;
using CodeRabbits.KaoList.Web.Models;
using CodeRabbits.KaoList.Web.Models.Songs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private async Task<IEnumerable<SongResource>> GetSongItemsByIdAsync(IEnumerable<string> ids)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<KaoListDataContext>();

            var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where ids.Contains(sing.Id)
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst
                               }).ToListAsync();

            return songs.Select(song => new SongResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = song.Sing.Id
            }).ToList();
        }

        private async Task<IEnumerable<SongResource>> GetSongItemsBySnippetAsync(IEnumerable<string> ids)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<KaoListDataContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<KaoListUser>>();
            var songs = await (from sing in context.Sings
                               join inst in context.Instrumental on sing.InstrumentalId equals inst.Id
                               where ids.Contains(sing.Id)
                               select new
                               {
                                   Sing = sing,
                                   Instrumental = inst
                               }).ToListAsync();

            SongSnippet snippet = new SongSnippet
            {

            };

            return songs.Select(song => new SongResource
            {
                Etag = song.Instrumental.ConcurrencyStamp,
                Id = song.Sing.Id,
                Snippet = snippet
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
        [ValidateAntiForgeryToken]
        public async Task<SongListResponse> GetListAsync(
            [FromQuery(Name = "part")] SongPart[] parts,
            [FromQuery(Name = "id")] string[]? ids,
            [FromQuery] string? myRating,
            [FromQuery] string? pageToken,
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
                            var songItemsById = await GetSongItemsByIdAsync(ids);
                            items.AddRange(songItemsById.Take(maxResults));
                            break;

                        case SongPart.Snippet:

                            break;

                        case SongPart.Statistics:

                            break;
                    }

                }
            }

            int totalResults = await GetTotalResultsFromDBAsync(ids);
            int resultsPerPage = items.Count;

            return new SongListResponse
            {
                Etag = new Guid().ToString(),
                Items = items,
                PageInfo = new PageInfo
                {
                    TotalResults = totalResults,
                    ResultPerPage = resultsPerPage
                }
            };
        }
    }
}
