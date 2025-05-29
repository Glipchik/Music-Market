namespace UserService.Business.Models.Account;

public record LoginResult(
    bool Success,
    string? RedirectUrl = null,
    string? ErrorMessage = null,
    string? ClientId = null);