// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.


using System.Security.Claims;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Web.Models.MyPages;
using CodeRabbits.KaoList.Web.Models.Thumbnails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyPageController : ControllerBase
    {
        private readonly KaoListDataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly UserManager<KaoListUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MyPageController(
            KaoListDataContext context,
            IServiceScopeFactory serviceScopeFactory,
            UserManager<KaoListUser> userManager,
            IWebHostEnvironment hostEnvironment
            )
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
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
                Resources = songSearchLogList
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
                Resources = signInLogList
            };

            return Ok(response);
        }

        [HttpGet("followedSongList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MyPageFollowedSongResponse), StatusCodes.Status200OK)]
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
                Resources = followedSongList
            };

            return Ok(response);
        }

        [HttpGet("profile")]
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
                Resource = new MyPageProfileResource
                {
                    Id = user.Id,
                    Email = user.Email,
                    Nickname = user.NickName,
                    NicknameEditedDateTime = user.NickNameEditedDatetime,
                    Thumbnail = !string.IsNullOrEmpty(user.ProfileIcon) ? new ThumbnailResource
                    {
                        Url = user.ProfileIcon,
                        Width = 120,
                        Height = 120
                    } : null
                }
            };

            return Ok(response);
        }

        [HttpPost("setProfileImage")]
        public async Task<IActionResult> SetProfileIconAsync([FromForm] MyPageProfileImage image)
        {
            try
            {
                if (image == null || image.Image == null)
                {
                    return BadRequest(new { Message = "Image is null or empty" });
                }

                var extension = Path.GetExtension(image.Image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || (extension != ".jpg" && extension != ".png"))
                {
                    return BadRequest(new { Message = "Only .jpg and .png extensions are allowed" });
                }

                if (image.Image.Length > 2_000_000)
                {
                    return BadRequest(new { Message = "File size exceeds the 2MB limit" });
                }

                var folderPath = _hostEnvironment.WebRootPath;
                var fileNameWithoutProfiles = $"{Guid.NewGuid()}{extension}";
                var fileNameWithProfiles = $"/profiles/{fileNameWithoutProfiles}";
                var imagePath = Path.Combine(folderPath, "profiles", fileNameWithoutProfiles);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.Image.CopyToAsync(stream);
                }

                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.ProfileIcon = fileNameWithProfiles;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest(new { Message = "User update failed", Errors = result.Errors });
                    }
                }

                return Ok(new { Message = "Upload successful" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Image upload failed", Error = e.Message });
            }
        }

        [HttpGet("getProfileImage")]
        public async Task<IActionResult> GetProfileImageAsync(string id)
        {
            KaoListUser? user;
            if (string.IsNullOrEmpty(id))
            {
                user = await _userManager.GetUserAsync(User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(id);
            }

            if (user == null || string.IsNullOrEmpty(user.ProfileIcon))
            {
                return NotFound();
            }
            
            return Ok(new { ImageUrl = user.ProfileIcon });
        }
    }
}
