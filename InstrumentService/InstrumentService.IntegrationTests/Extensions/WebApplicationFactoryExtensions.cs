using System.Net.Http.Headers;
using InstrumentService.IntegrationTests.Factories;

namespace InstrumentService.IntegrationTests.Extensions;

public static class WebApplicationFactoryExtensions
{ 
    public static HttpClient CreateAuthenticatedClient(this CustomWebApplicationFactory factory, string userId)
    {
        var client = factory.CreateClient();
        var token = JwtTokenFactory.CreateToken(
            userId: userId,
            scopes: ["instrumentapi.read", "instrumentapi.write"],
            expires: DateTime.UtcNow.AddMinutes(30)
        );
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}