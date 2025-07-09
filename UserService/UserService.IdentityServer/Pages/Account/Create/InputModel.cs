using System.ComponentModel.DataAnnotations;

namespace UserService.IdentityServer.Pages.Account.Create;

public class InputModel
{
    [Required] public string Username { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = null!;
    public string? Email { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }
}