using Duende.IdentityServer.Models;

namespace UserService.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("api1", "My API")
    ];

    
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

            AllowedScopes =
            {
                "openid",
                "profile",
                "api1",
                "offline_access"
            },

            AllowOfflineAccess = true,
            RequireClientSecret = true
        }
    ];
}