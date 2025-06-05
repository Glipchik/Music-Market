using Duende.Bff;

namespace UserService.Bff;

internal class FrontendHostReturnUrlValidator : IReturnUrlValidator
{
    public Task<bool> IsValidAsync(string returnUrl)
    {
        var uri = new Uri(returnUrl);
        return Task.FromResult(uri is { Host: "localhost", Port: 4200 });
    }
}