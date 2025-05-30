using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserService.Business.Abstractions;
using UserService.Business.Entities;
using UserService.Business.Services;

namespace UserService.IdentityServer.Pages.Account.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index(
    IAccountService accountService,
    IIdentityServerInteractionService interaction,
    IAuthenticationSchemeProvider schemeProvider,
    IIdentityProviderStore identityProviderStore,
    IEventService events,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : PageModel
{
    public ViewModel View { get; set; } = default!;

    [BindProperty] public InputModel Input { get; set; } = default!;

    public IActionResult OnGet(string? returnUrl)
    {
        BuildModel(returnUrl);

        return Page();
    }

    private void BuildModel(string? returnUrl)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl
        };

        View = new ViewModel();
    }

    public async Task<IActionResult> OnPost()
    {
        var context = await interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (Input.Button != "login")
        {
            if (context != null)
            {
                ArgumentNullException.ThrowIfNull(Input.ReturnUrl, nameof(Input.ReturnUrl));
                await interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                return context.IsNativeClient() ? this.LoadingPage(Input.ReturnUrl) : Redirect(Input.ReturnUrl ?? "~/");
            }

            return Redirect("~/");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await accountService.LoginAsync(
            Input.Username!,
            Input.Password!,
            Input.ReturnUrl,
            Url.IsLocalUrl);

        if (result.Success)
        {
            return Redirect(result.RedirectUrl!);
        }

        ModelState.AddModelError(string.Empty, result.ErrorMessage!);
        BuildModel(Input.ReturnUrl);
        return Page();
    }
}