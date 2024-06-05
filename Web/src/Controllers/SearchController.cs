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

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SearchListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SearchListResponse>> GetListAsync(
            [FromQuery(Name = "q")] string[]? query,
            [FromQuery(Name = "page")] int page = 1,
            int maxResults = 20
        )
        {
            if (query is null || !query.Any())
            {
                return BadRequest("No query provided.");
            }

            var offset = (page - 1) * maxResults;

            var searchResponse = await _searchService.SearchAsync(query, offset, maxResults);

            return Ok(searchResponse);
        }
    }
}
