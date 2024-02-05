// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account;

public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<KaoListUser> _userManager;
    private readonly ILogger<ConfirmEmailModel> _logger;

    public ConfirmEmailModel(UserManager<KaoListUser> userManager, ILogger<ConfirmEmailModel> logger)
    {
        _userManager = userManager;
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
            _logger.LogWarning(e.Message);
            _logger.LogWarning("code = {}", code);
            StatusMessage = "이메일 확인에 실패했습니다. 다시 시도해 주세요.";
            return RedirectToPage("./ConfirmEmailFailed");
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if(result.Succeeded)
        {
            UserName = user.UserName;
        }
        StatusMessage = result.Succeeded ? "이메일이 확인 되었습니다. 감사합니다." : "이메일 확인에 실패했습니다.";
        return Page();
    }
}
