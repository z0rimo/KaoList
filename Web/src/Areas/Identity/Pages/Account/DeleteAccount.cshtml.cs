using System.ComponentModel.DataAnnotations;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages.Account;

[Authorize]
public class DeleteAccountModel : PageModel
{
    private readonly UserManager<KaoListUser> _userManager;
    private readonly SignInManager<KaoListUser> _signInManager;
    private readonly KaoListDataContext _context;

    public DeleteAccountModel(
        UserManager<KaoListUser> userManager,
        SignInManager<KaoListUser> signInManager,
        KaoListDataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        Input = new InputModel();
    }

    public class InputModel
    {
        [Required]
        public string? Email { get; set; }

        public string? Reasons { get; set; }

        public bool Agree { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            Input = new InputModel
            {
                Email = user.Email!
            };
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || !Input.Agree)
        {
            return RedirectToPage("/GeneralError");
        }

        if (string.IsNullOrWhiteSpace(Input.Email))
        {
            return RedirectToPage("/GeneralError");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null || user.Email != Input.Email)
        {
            return NotFound();
        }

        if (!Request.Form.ContainsKey("agree") || Request.Form["agree"].ToString() != "on")
        {
            return RedirectToPage("/GeneralError");
        }

        var deletionReason = new KaoListUserDeleteReason
        {
            UserId = user.Id,
            Reason = string.Join(",", Input.Reasons)
        };

        _context.UserDeleteReason.Add(deletionReason);
        await _context.SaveChangesAsync();

        var logins = await _userManager.GetLoginsAsync(user);
        foreach (var login in logins)
        {
            var result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
            if (!result.Succeeded)
            {
                return RedirectToPage("./DeleteAccount", new { Failure = true });
            }
        }

        var deleteResult = await _userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
        {
            return RedirectToPage("./DeleteAccount", new { Failure = true });
        }

        await _signInManager.SignOutAsync();
        return RedirectToPage("./CompletedAccountDelete");
    }
}
