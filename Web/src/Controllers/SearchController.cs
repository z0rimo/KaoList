// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Searches;
using CodeRabbits.KaoList.Web.Services.Searches;
using Microsoft.AspNetCore.Mvc;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("songs")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongSearchListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SongSearchListResponse>> GetSongListAsync(
            [FromQuery(Name = "q")] string? query,
            [FromQuery(Name = "page")] int page = 1,
            int maxResults = 20
        )
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("No query provided.");
            }

            var offset = (page - 1) * maxResults;

            var searchResponse = await _searchService.SongSearchAsync(query, offset, maxResults);

            return Ok(searchResponse);
        }
    }
}
