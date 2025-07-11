using System.Net.Http.Headers;
using AnalyticsService.IntegrationTests.Factories;

namespace AnalyticsService.IntegrationTests.Extensions;

public static class WebApplicationFactoryExtensions
{
    public static HttpClient CreateAuthenticatedClient(this CustomWebApplicationFactory factory, string userId)
    {
        var client = factory.CreateClient();
        var token = JwtTokenFactory.CreateToken(
            userId: userId,
            scopes: ["analyticsapi.read"],
            expires: DateTime.UtcNow.AddMinutes(30)
        );
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}