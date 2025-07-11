using Microsoft.IdentityModel.Tokens;

namespace AnalyticsService.IntegrationTests.Constants;

public static class TestAuthConstants
{
    public const string Issuer = "https://test-issuer";
    public const string Audience = "analyticsapi";
    public const string UserId = "test-user-id";
    public const string SecretKey = "wmbOOD9cQ9OtHDlFCba4hgSvlHVZ1fll8skOAnxtCgU=";

    public static SymmetricSecurityKey SigningKey =>
        new(Convert.FromBase64String(SecretKey));
}