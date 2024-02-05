// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account;

public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<KaoListUser> _userManager;
    private readonly SignInManager<KaoListUser> _signInManager;
    private readonly ILogger<ConfirmEmailModel> _logger;

    public ConfirmEmailModel(
        UserManager<KaoListUser> userManager,
        SignInManager<KaoListUser> signInManager,
        ILogger<ConfirmEmailModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [TempData]
    public string StatusMessage { get; set; }
    public string UserName { get; set; }
    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Login");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }

        catch (FormatException e)
        {
            _logger.LogWarning("Code = {}, ConfirmEmail Message = {}", code, e.Message);
            StatusMessage = "이메일 확인에 실패했습니다. 다시 시도해 주세요.";
            return RedirectToPage("./ConfirmEmailFailed");
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            UserName = user.UserName;
            StatusMessage = SR.Your_email_has_been_confirmed;
        }
        else StatusMessage = SR.Email_verification_failed;
        return Page();
    }
}
