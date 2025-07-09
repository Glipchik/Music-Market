using Microsoft.AspNetCore.Identity;

namespace UserService.DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = null!;
}