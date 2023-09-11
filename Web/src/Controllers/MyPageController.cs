// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.


using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web.Models.MyPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyPageController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly UserManager<KaoListUser> _userManager;

        public MyPageController(
            KaoListDataContext context,
            IServiceScopeFactory serviceScopeFactory,
            UserManager<KaoListUser> userManager
            )
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
            _userManager = userManager;
        }

        private async Task<IActionResult> ValidateAndGetUserAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "로그인을 해주세요." });
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "존재하지 않는 아이디입니다. 다시 로그인해주세요." });
            }

            return Ok(user);
        }

        [HttpGet("songSearchLogList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MyPageSongSearchLogResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSongSearchLogAsync()
        {
            var userValidationResult = await ValidateAndGetUserAsync();
            if (userValidationResult is not OkObjectResult okResult || okResult.Value is not KaoListUser user)
            {
                return userValidationResult;
            }

            var userId = user.Id;

            var songSearchLogList = await (_context.SongSearchLogs
                                                .Where(log => log.UserId == userId)
                                                .Select(log => new MyPageSongSerachLogResource { Id = log.Id, Query = log.Query, Created = log.Created })
                                                .OrderByDescending(log => log.Created)
                                                .ToListAsync());

            var response = new MyPageSongSearchLogResponse
            {
                Etag = new Guid().ToString(),
                items = songSearchLogList
            };

            return Ok(response);
        }

        [HttpGet("signInLogList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MyPageSignInLogResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSignInLogListAsync()
        {
            var userValidationResult = await ValidateAndGetUserAsync();
            if (userValidationResult is not OkObjectResult okResult || okResult.Value is not KaoListUser user)
            {
                return userValidationResult;
            }

            var userId = user.Id;

            var signInLogList = await (_context.SignInAttempts
                                            .Where(log => log.UserId == userId)
                                            .Select(log => new MyPageSignInLogResource { Id = log.Id, Created = log.CreateTime, IpAddress = log.IpAddress })
                                            .OrderByDescending(log => log.Created)
                                            .ToListAsync());

            var response = new MyPageSignInLogResponse
            {
                Etag = new Guid().ToString(),
                items = signInLogList
            };

            return Ok(response);
        }

        [HttpGet("followedSongList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MyPageSignInLogResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFollowedSongListAsync()
        {
            var userValidationResult = await ValidateAndGetUserAsync();
            if (userValidationResult is not OkObjectResult okResult || okResult.Value is not KaoListUser user)
            {
                return userValidationResult;
            }

            var userId = user.Id;

            var followedSongList = await (_context.SingFollowers
                                               .Where(f => f.UserId == userId)
                                               .Join(_context.Sings, f => f.SingId, s => s.Id, (f, s) => new { f, s })
                                               .Join(_context.Instrumental, fs => fs.s.InstrumentalId, i => i.Id, (fs, i) =>
                                                     new MyPageFollowedSongResource { Title = i.Title, Id = fs.s.Id, Created = fs.f.Created })
                                               .OrderByDescending(fs => fs.Created)
                                               .ToListAsync());

            var response = new MyPageFollowedSongResponse
            {
                Etag = new Guid().ToString(),
                items = followedSongList
            };

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MyPageProfileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyPageListAsync()
        {
            var userValidationResult = await ValidateAndGetUserAsync();
            if (userValidationResult is not OkObjectResult okResult || okResult.Value is not KaoListUser user)
            {
                return userValidationResult;
            }

            var response = new MyPageProfileResponse
            {
                Etag = new Guid().ToString(),
                Item = new MyPageProfileResource
                {
                    Email = user.Email,
                    Nickname = user.NickName,
                    NicknameEditedDateTime = user.NickNameEditedDatetime
                }
            };

            return Ok(response);
        }
    }
}
