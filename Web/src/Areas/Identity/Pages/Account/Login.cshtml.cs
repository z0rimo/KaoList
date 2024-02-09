// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Net;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Duende.IdentityServer.Models.IdentityResources;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<KaoListUser> _signInManager;
        private readonly UserManager<KaoListUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly KaoListDataContext _context;

        public LoginModel(
            SignInManager<KaoListUser> signInManager,
            UserManager<KaoListUser> userManager,
            ILogger<LoginModel> logger,
            KaoListDataContext context
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0}을 입력해주십시오.")]
            [EmailAddress(ErrorMessage = "올바른 이메일 형식이 아닙니다.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0}을 입력해주십시오.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "로그인 상태 유지")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                var user = await _userManager.FindByEmailAsync(Input.Email);
                var userId = user?.Id;
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4();

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    await SaveSignInAttempt(Input.Email, userId, true, remoteIpAddress);
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    await SaveSignInAttempt(Input.Email,userId, false, remoteIpAddress);
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "이메일 또는 비밀번호를 잘못 입력하셨습니다.");
                    await SaveSignInAttempt(Input.Email, userId, false, remoteIpAddress);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task SaveSignInAttempt(string email, string UserId, bool succeeded, IPAddress IpAddress)
        {
            var user = await _userManager.FindByEmailAsync(email);
            _ = user?.Id;

            var singInAttempt = new SignInAttempt
            {
                UserId = UserId,
                Successed = succeeded,
                IpAddress = IpAddress,
                CreateTime = DateTime.UtcNow
            };

            _context.SignInAttempts.Add(singInAttempt);
            await _context.SaveChangesAsync();
        }
    }
}
