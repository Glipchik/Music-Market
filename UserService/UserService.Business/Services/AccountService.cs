using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using UserService.Business.Abstractions;
using UserService.Business.Entities;
using UserService.Business.Models.Account;

namespace UserService.Business.Services;

public class AccountService(
    IIdentityServerInteractionService interaction,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) : IAccountService
{
    public async Task<LoginResult> LoginAsync(string username, string password, string? returnUrl,
        Func<string, bool> isLocalUrl)
    {
        var context = await interaction.GetAuthorizationContextAsync(returnUrl);

        var result = await signInManager.PasswordSignInAsync(
            username,
            password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return new LoginResult
            (
                Success: false,
                ErrorMessage: "Invalid username or password",
                ClientId: context?.Client.ClientId
            );
        }

        var redirectUrl = !string.IsNullOrEmpty(returnUrl) && (context != null || isLocalUrl(returnUrl))
            ? returnUrl
            : "~/";

        return new LoginResult(Success: true, RedirectUrl: redirectUrl);
    }

    public async Task<RegistrationResult> RegisterAsync(
        string username,
        string password,
        string? email,
        string? returnUrl,
        Func<string, bool> isLocalUrl)
    {
        var context = await interaction.GetAuthorizationContextAsync(returnUrl);

        var existingUser = await userManager.FindByNameAsync(username);
        
        if (existingUser != null)
        {
            return new RegistrationResult
            (
                Success: false,
                ErrorMessage: "Username already exists."
            );
        }

        var user = new ApplicationUser
        {
            UserName = username,
            Email = email
        };

        var result = await userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
        {
            return new RegistrationResult
            (
                Success: false,
                ErrorMessage: result.Errors.FirstOrDefault()?.Description ?? "Unknown error occured."
            );
        }

        await signInManager.SignInAsync(user, isPersistent: false);

        var redirectUrl = !string.IsNullOrEmpty(returnUrl) && (context != null || isLocalUrl(returnUrl))
            ? returnUrl
            : "~/";

        return new RegistrationResult
        (
            Success: true,
            RedirectUrl: redirectUrl
        );
    }
    
    public async Task<bool> ShouldShowLogoutPromptAsync(ClaimsPrincipal user, string? logoutId)
    {
        if (user.Identity?.IsAuthenticated != true)
            return false;

        var context = await interaction.GetLogoutContextAsync(logoutId);
        return context.ShowSignoutPrompt;
    }

    public async Task<LogoutResult> LogoutAsync(
        ClaimsPrincipal user,
        string? logoutId,
        Func<object, string> redirectUrlBuilder)
    {
        logoutId ??= await interaction.CreateLogoutContextAsync();

        if (user.Identity is { IsAuthenticated: true })
        {
            await signInManager.SignOutAsync();
        }
        
        return new LogoutResult(
            RedirectUrl: redirectUrlBuilder(new { logoutId })
        );
    }
}