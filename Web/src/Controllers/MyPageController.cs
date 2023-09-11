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

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MyPageListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyPageListAsync()
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

            var songSearchLogList = await (_context.SongSearchLogs
                                                .Where(log => log.UserId == userId)
                                                .Select(log => new MyPageSongSerachLog { Query = log.Query, Created = log.Created })
                                                .ToListAsync());

            var signInLogList = await (_context.SignInAttempts
                                            .Where(log => log.UserId == userId)
                                            .Select(log => new MyPageSignInLog { Created = log.CreateTime, IpAddress = log.IpAddress })
                                            .ToListAsync());

            var followedSongList = await (_context.SingFollowers
                                               .Where(f => f.UserId == userId)
                                               .Join(_context.Sings, f => f.SingId, s => s.Id, (f, s) => new { f, s })
                                               .Join(_context.Instrumental, fs => fs.s.InstrumentalId, i => i.Id, (fs, i) =>
                                                     new MyPageFollowedSong { Title = i.Title, Created = fs.f.Created })
                                               .ToListAsync());

            var response = new MyPageListResponse
            {
                Etag = new Guid().ToString(),
                Item = new MyPageResource
                {
                    Email = user.Email,
                    Nickname = user.NickName,
                    NicknameEditedDateTime = user.NickNameEditedDatetime,
                    SongSearchQueryList = songSearchLogList,
                    SignInLogList = signInLogList,
                    FollowedSongList = followedSongList
                }
            };

            return Ok(response);
        }
         
    }
}
