using InstrumentService.DataAccess.Services.Models;

namespace InstrumentService.DataAccess.Abstractions;

public interface ITokenService
{
    Task<TokenResponseModel> GetAccessTokenAsync(CancellationToken cancellationToken);
    Task InvalidateCachedTokenAsync(CancellationToken cancellationToken);
}