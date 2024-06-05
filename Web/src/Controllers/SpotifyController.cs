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
        private readonly ILogger<SpotifyController> _logger;

        public SpotifyController(SpotifyService spotifyService, ILogger<SpotifyController> logger)
        {
            _spotifyService = spotifyService;
            _logger = logger;
        }

        [HttpGet("album-image")]
        public async Task<IActionResult> GetTopAlbumImage([FromQuery] string query)
        {
            _logger.LogInformation($"Received request for query: {query}");

            var imageUrl = await _spotifyService.GetTopTrackAlbumImageAsync(query);
            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogWarning($"No album found for query: {query}");
                return NotFound("No album found for the given query.");
            }

            return Ok(imageUrl);
        }
    }

}
