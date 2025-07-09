using System.Security.Claims;
using UserService.Business.Models.Account;

namespace UserService.Business.Abstractions;

public interface IAccountService
{
    Task<LoginResult> LoginAsync(string username, string password, string? returnUrl, Func<string, bool> isLocalUrl);

    Task<RegistrationResult> RegisterAsync(string username, string password, string name, string? email, string? returnUrl,
        Func<string, bool> isLocalUrl);

    Task<bool> ShouldShowLogoutPromptAsync(ClaimsPrincipal user, string? logoutId);

    Task<LogoutResult> LogoutAsync(
        ClaimsPrincipal user,
        string? logoutId,
        Func<object, string> redirectUrlBuilder);
}