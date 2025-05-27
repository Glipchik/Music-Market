using Duende.Bff;

namespace UserService.Bff.Models;

public class Api
{
    public string? LocalPath { get; set; }
    public string? RemoteUrl { get; set; }
    public TokenType RequiredToken { get; set; }
}