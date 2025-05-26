using Duende.IdentityServer.Models;

namespace UserService.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources => [];

    public static IEnumerable<ApiScope> ApiScopes => [];

    public static IEnumerable<Client> Clients => [];
}