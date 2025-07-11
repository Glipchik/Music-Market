using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AnalyticsService.IntegrationTests.Constants;
using Microsoft.IdentityModel.Tokens;

namespace AnalyticsService.IntegrationTests.Factories;

public static class JwtTokenFactory
{
    public static string CreateToken(
        string userId,
        IList<string>? scopes = null,
        DateTime? expires = null,
        string? audience = null,
        string? issuer = null)
    {
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, userId) };

        if (scopes != null)
        {
            claims.AddRange(scopes.Select(scope => new Claim("scope", scope)));
        }

        var token = new JwtSecurityToken(
            issuer: issuer ?? TestAuthConstants.Issuer,
            audience: audience ?? TestAuthConstants.Audience,
            claims: claims,
            expires: expires ?? DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                TestAuthConstants.SigningKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}