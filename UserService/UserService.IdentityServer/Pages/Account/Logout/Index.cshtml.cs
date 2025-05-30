using Duende.IdentityModel;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserService.Business.Abstractions;
using UserService.IdentityServer.Pages.Logout;

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
        LogoutId ??= null;

        var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
        
        var externalSignOutSupported = false;

        if (!string.IsNullOrEmpty(idp) && idp != Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider)
        {
            externalSignOutSupported = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
        }

        var result = await accountService.LogoutAsync(
            User,
            LogoutId,
            externalSignOutSupported,
            routeValues => Url.Page("/Account/Logout/LoggedOut", routeValues)!);

        if (result.RequiresExternalSignOut)
        {
            return SignOut(new AuthenticationProperties { RedirectUri = result.RedirectUrl }, result.ExternalScheme!);
        }

        return Redirect(result.RedirectUrl);
    }
}