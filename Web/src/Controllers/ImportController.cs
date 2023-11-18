// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly SongService _songService;
        private readonly UserService _userService;

        public ImportController(SongService songService, UserService userService)
        {
            _songService = songService;
            _userService = userService;
        }

        

        [HttpPost("user")]
        public IActionResult ImportUsersFromJson()
        {
            _userService.ConvertAndSaveJsonToDb();

            return Ok("Users imported successfully");
        }

        [HttpDelete]
        public IActionResult deleteAll()
        {
            _songService.DeleteAll();

            return Ok("Songs deleted successfully");
        }
    }
}
