namespace UserService.Business.Models.Account;

public record LogoutResult(bool RequiresExternalSignOut, string RedirectUrl, string? ExternalScheme = null);