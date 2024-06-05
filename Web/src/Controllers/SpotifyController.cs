// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotifyController : ControllerBase
    {
        private readonly SpotifyService _spotifyService;

        public SpotifyController(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("track-album-image")]
        public async Task<IActionResult> GetTopTrackAlbumImage([FromQuery] string query)
        {
            var imageUrl = await _spotifyService.GetTopTrackAlbumImageAsync(query);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return NotFound("No album found for the given query.");
            }

            return Ok(imageUrl);
        }
    }

}
