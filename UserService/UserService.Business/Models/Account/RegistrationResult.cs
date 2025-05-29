namespace UserService.Business.Models.Account;

public record RegistrationResult(bool Success, string? RedirectUrl = null, string? ErrorMessage = null);