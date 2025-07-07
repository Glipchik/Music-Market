using Microsoft.IdentityModel.Tokens;

namespace InstrumentService.IntegrationTests.Constants;

public static class AuthTestConstants
{
    public const string Issuer = "https://test-issuer";
    public const string Audience = "instrumentapi";
    public const string UserId = "test-user-id";
    public const string OtherUserId = "test-other-user-id";
    public const string SecretKey = "wmbOOD9cQ9OtHDlFCba4hgSvlHVZ1fll8skOAnxtCgU=";

    public static SymmetricSecurityKey SigningKey =>
        new(Convert.FromBase64String(SecretKey));
}