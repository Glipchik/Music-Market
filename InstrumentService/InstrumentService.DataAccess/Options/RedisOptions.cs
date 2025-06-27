namespace InstrumentService.DataAccess.Options;

public class RedisOptions
{
    public string Configuration { get; set; } = default!;
    public TokenCacheOptions TokenCacheOptions { get; set; } = default!;
}