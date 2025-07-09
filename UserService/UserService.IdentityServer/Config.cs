using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace UserService.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new("roles", ["role"]) { DisplayName = "Your user roles" }
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new("instrumentapi.read", "Read access to Instrument API"),
            new("instrumentapi.write", "Write access to Instrument API"),
            new("analyticsapi.read", "Read access to Analytics API"),
            new("userapi.read", "Read access to User API"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("instrumentapi", "Instrument API")
            {
                Scopes = { "instrumentapi.read", "instrumentapi.write" },
                UserClaims = { "role" }
            },
            new("analyticsapi", "Analytics API")
            {
                Scopes = { "analyticsapi.read" }
            },
            new("userapi", "User API")
            {
                Scopes = { "userapi.read" }
            },
        };

    public static IEnumerable<Client> Clients =>
    [
        new()
        {
            ClientId = "bff_client",
            ClientName = "BFF Client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RedirectUris = { "https://localhost:5000/signin-oidc" },
            FrontChannelLogoutUri = "https://localhost:5000/signout-oidc",
            PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "roles",
                "instrumentapi.read",
                "instrumentapi.write"
            },
            RequireClientSecret = true
        },
        new()
        {
            ClientId = "instrument_client",
            ClientName = "Instrument Service Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "analyticsapi.read", "userapi.read" }
        }
    ];
}