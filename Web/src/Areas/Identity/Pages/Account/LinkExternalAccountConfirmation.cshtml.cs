using System.Security.Claims;
using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account;

public class LinkExternalAccountConfirmationModel : PageModel
{
    private readonly ILogger<LinkExternalAccountConfirmationModel> _logger;
    private readonly SignInManager<KaoListUser> _signInManager;

    public LinkExternalAccountConfirmationModel(
        ILogger<LinkExternalAccountConfirmationModel> logger,
        SignInManager<KaoListUser> signInManager
    )
    {
        _logger = logger;
        _signInManager = signInManager;
    }

    public string? ExternalProvider { get; set; }

    public string? UserEmail { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToPage("./Login");
        }

        ExternalProvider = info.LoginProvider;
        UserEmail = info.Principal.FindFirstValue(ClaimTypes.Email);

        _logger.LogInformation("Associate a {UserEmail} with an {ExternalProvider} account.", UserEmail, ExternalProvider);

        return Page();
    }
}
