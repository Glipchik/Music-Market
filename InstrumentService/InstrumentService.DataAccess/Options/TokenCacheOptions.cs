namespace InstrumentService.DataAccess.Options;

public class TokenCacheOptions
{
    public string CacheKey { get; set; } = default!;
    public int ExpirationBufferSeconds { get; set; }
}