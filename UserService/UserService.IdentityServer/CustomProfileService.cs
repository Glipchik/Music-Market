using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using UserService.Business.Entities;

namespace UserService.IdentityServer;

public class CustomProfileService(UserManager<ApplicationUser> userManager) : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        if (user == null) return;

        var claims = await userManager.GetClaimsAsync(user);
        claims.Add(new Claim(JwtClaimTypes.Name, user.UserName!));

        context.IssuedClaims.AddRange(claims);

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            if (!context.IssuedClaims.Any(claim => claim.Type == JwtClaimTypes.Role && claim.Value == role))
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
            }
        }

        context.IssuedClaims = context.IssuedClaims
            .Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        context.IsActive = user != null;
    }
}