// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogService _logService;

        public LogsController(LogService logService)
        {
            _logService = logService;
        }

        [HttpPost("applog")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAppLogAsync([FromBody] AppLog logRequest)
        {
            if (logRequest is null || string.IsNullOrEmpty(logRequest.Log))
            {
                return BadRequest("LogRequest empty");
            }

            try
            {
                await _logService.CreateAppLogAsync(logRequest.Log);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
