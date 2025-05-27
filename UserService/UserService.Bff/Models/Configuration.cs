namespace UserService.Bff.Models;

public class Configuration
{
    public string? Authority { get; set; }

    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }

    public List<string> Scopes { get; set; } = [];
    public List<Api> Apis { get; set; } = [];
}

