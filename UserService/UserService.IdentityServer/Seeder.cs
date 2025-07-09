using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UserService.DataAccess.Entities;
using UserService.IdentityServer.Options;

namespace UserService.IdentityServer;

public class Seeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        var roles = new List<string> { "User", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }
        }
    }

    public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var adminOptions = configuration.GetSection(nameof(AdminOptions)).Get<AdminOptions>();

        if (adminOptions is null)
            throw new InvalidOperationException("AdminOptions section is missing or invalid.");

        var adminEmail = adminOptions.Email;
        var adminPassword = adminOptions.Password;
        var adminName = adminOptions.Name;

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = adminName,
                EmailConfirmed = true,
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (!createResult.Succeeded)
            {
                throw new Exception("Cannot create admin: " +
                                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}