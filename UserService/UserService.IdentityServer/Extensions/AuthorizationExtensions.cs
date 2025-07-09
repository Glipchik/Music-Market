using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserService.IdentityServer.Options;

namespace UserService.IdentityServer.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>()!;

        services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = authOptions.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidAudiences = authOptions.ValidAudiences,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("ReadAccess", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "userapi.read");
            });

        return services;
    }
}