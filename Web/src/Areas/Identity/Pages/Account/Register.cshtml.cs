// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<KaoListUser> _signInManager;
        private readonly UserManager<KaoListUser> _userManager;
        private readonly IUserStore<KaoListUser> _userStore;
        private readonly IUserEmailStore<KaoListUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<KaoListUser> userManager,
            IUserStore<KaoListUser> userStore,
            SignInManager<KaoListUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(SR), ErrorMessageResourceName = "_0__is_required")]
            [EmailAddress]
            [Display(Name = "이메일")]
            public string Email { get; set; }

            [Required(ErrorMessage = "* {0}를 입력하세요.")]
            [StringLength(100, ErrorMessage = "* {0}는 최소 {2}자에서 {1}자 사이의 조합이어야합니다.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "비밀번호 확인")]
            [Compare("Password", ErrorMessage = "* 비밀번호와 일치하지 않습니다.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "* {0}을 입력하세요.")]
            [Display(Name = "닉네임")]
            public string Nickname { get; set; }

            [Required]
            [Display(Name = nameof(TermsOfService))]
            public bool TermsOfService { get; set; }

            [Required]
            [Display(Name = nameof(PrivacyPolicy))]
            public bool PrivacyPolicy { get; set; }

            [Display(Name = nameof(AdReceivedAccept))]
            public bool AdReceivedAccept { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.NickName = Input.Nickname;
                user.NormalizedNickName = Input.Nickname.ToUpperInvariant();
                user.NickNameEditedDatetime = DateTime.UtcNow;
                user.AcceptEmail = Input.AdReceivedAccept;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private KaoListUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<KaoListUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(KaoListUser)}'. " +
                    $"Ensure that '{nameof(KaoListUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<KaoListUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<KaoListUser>)_userStore;
        }
    }
}
