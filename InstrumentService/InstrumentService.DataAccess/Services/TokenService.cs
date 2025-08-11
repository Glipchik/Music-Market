using Duende.IdentityModel.Client;
using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Options;
using InstrumentService.DataAccess.Services.Models;
using Microsoft.Extensions.Options;

namespace InstrumentService.DataAccess.Services;

public class TokenService(HttpClient httpClient, IOptions<ClientCredentialsOptions> clientCredentialsOptions)
    : ITokenService
{
    private readonly ClientCredentialsOptions _clientCredentialsOptionsValue = clientCredentialsOptions.Value;

    public async Task<TokenResponseModel> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        var disco = await httpClient.GetDiscoveryDocumentAsync(cancellationToken: cancellationToken);

        if (disco.IsError)
        {
            throw new Exception($"Discovery document request failed: {disco.Error}");
        }

        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = _clientCredentialsOptionsValue.ClientId,
            ClientSecret = _clientCredentialsOptionsValue.ClientSecret,
            Scope = _clientCredentialsOptionsValue.Scope
        }, cancellationToken: cancellationToken);

        if (tokenResponse.IsError)
        {
            throw new Exception($"Token request failed: {tokenResponse.Error}");
        }

        if (string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
        {
            throw new InvalidOperationException("Access token is null or empty.");
        }

        return new TokenResponseModel(tokenResponse.AccessToken, tokenResponse.ExpiresIn);
    }

    public Task InvalidateCachedTokenAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}