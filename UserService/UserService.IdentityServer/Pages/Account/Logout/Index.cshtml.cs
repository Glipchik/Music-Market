using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserService.Business.Abstractions;
using UserService.Business.Constants;

namespace UserService.IdentityServer.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class Index(IAccountService accountService)
    : PageModel
{
    [BindProperty] public string? LogoutId { get; set; }

    public async Task<IActionResult> OnGet(string? logoutId)
    {
        LogoutId = logoutId;

        var showLogoutPrompt = await accountService.ShouldShowLogoutPromptAsync(User, LogoutId);

        if (!showLogoutPrompt)
        {
            return await OnPost();
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await accountService.LogoutAsync(
            User,
            LogoutId,
            routeValues => Url.Page(RouteConstants.AccountLoggedOut, routeValues)!);
        
        return Redirect(result.RedirectUrl);
    }
}