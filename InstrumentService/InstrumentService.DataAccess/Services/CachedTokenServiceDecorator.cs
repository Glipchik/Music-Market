using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Options;
using InstrumentService.DataAccess.Services.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace InstrumentService.DataAccess.Services;

public class CachedTokenServiceDecorator(
    ITokenService innerTokenService,
    IDistributedCache distributedCache,
    IOptions<RedisOptions> redisOptions) : ITokenService
{
    private readonly RedisOptions _redisOptionsValue = redisOptions.Value;
    public async Task<TokenResponseModel> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        var cachedToken = await distributedCache
            .GetStringAsync(_redisOptionsValue.TokenCacheOptions.CacheKey, cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(cachedToken))
        {
            return new TokenResponseModel(cachedToken, 0);
        }

        var token = await innerTokenService.GetAccessTokenAsync(cancellationToken);

        var ttl = TimeSpan.FromSeconds(token.ExpiresIn - _redisOptionsValue.TokenCacheOptions.ExpirationBufferSeconds);
        await distributedCache.SetStringAsync(_redisOptionsValue.TokenCacheOptions.CacheKey, token.AccessToken,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            }, cancellationToken);

        return token;
    }

    public async Task InvalidateCachedTokenAsync(CancellationToken cancellationToken)
    {
        await distributedCache.RemoveAsync(_redisOptionsValue.TokenCacheOptions.CacheKey, cancellationToken);
    }
}