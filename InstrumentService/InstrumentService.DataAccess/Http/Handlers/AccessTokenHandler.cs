using System.Net;
using Duende.IdentityModel.Client;
using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Extensions;

namespace InstrumentService.DataAccess.Http.Handlers;

public class AccessTokenHandler(ITokenService tokenService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await tokenService.GetAccessTokenAsync(cancellationToken);
        request.SetBearerToken(token.AccessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized) return response;

        await tokenService.InvalidateCachedTokenAsync(cancellationToken);
        var newToken = await tokenService.GetAccessTokenAsync(cancellationToken);

        var clonedRequest = await request.CloneHttpRequestMessage(cancellationToken);
        clonedRequest.SetBearerToken(newToken.AccessToken);
        response = await base.SendAsync(clonedRequest, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized) return response;

        await tokenService.InvalidateCachedTokenAsync(cancellationToken);

        throw new UnauthorizedAccessException(
            "Access denied after refreshing token. The token might be invalid or insufficient.");
    }
}