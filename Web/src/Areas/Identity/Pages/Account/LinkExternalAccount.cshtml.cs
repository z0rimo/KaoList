// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account;

public class LinkExternalAccountModel : PageModel
{
    private readonly UserManager<KaoListUser> _userManager;
    private readonly SignInManager<KaoListUser> _signInManager;

    public LinkExternalAccountModel(
        UserManager<KaoListUser> userManager,
        SignInManager<KaoListUser> signInManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string? ReturnUrl { get; set; }

    public string? Email { get; set; }

    public void OnGet(string email, string returnUrl)
    {
        Email = email;
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string email, string returnUrl)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToPage("./Login");
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "No users with that email were found.");
            return Page();
        }

        var result = await _userManager.AddLoginAsync(user, info);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl ?? "/");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
