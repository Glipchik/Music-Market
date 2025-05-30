using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserService.Business.Abstractions;
using UserService.Business.Entities;

namespace UserService.IdentityServer.Pages.Account.Create;

[SecurityHeaders]
[AllowAnonymous]
public class Index(
    IIdentityServerInteractionService interaction,
    IAccountService accountService)
    : PageModel
{
    [BindProperty] public InputModel Input { get; set; } = default!;

    public IActionResult OnGet(string? returnUrl)
    {
        Input = new InputModel { ReturnUrl = returnUrl };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var context = await interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (Input.Button != "create")
        {
            if (context == null) return Redirect("~/");
            await interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

            return context.IsNativeClient() ? this.LoadingPage(Input.ReturnUrl) : Redirect(Input.ReturnUrl ?? "~/");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var result = await accountService.RegisterAsync(
            Input.Username,
            Input.Password,
            Input.Email,
            Input.ReturnUrl,
            Url.IsLocalUrl
        );

        if (result.Success) return Redirect(result.RedirectUrl ?? "~/");
        
        if (!string.IsNullOrEmpty(result.ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
        }

        return Page();
    }
}
