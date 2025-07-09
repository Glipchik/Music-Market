using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Business.Abstractions;
using UserService.Business.Models;

namespace UserService.IdentityServer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("{userId}/contacts")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<UserContactsModel> GetContacts(string userId, CancellationToken cancellationToken)
    {
        var response = await userService.GetUserContactsAsync(userId, cancellationToken);

        return response;
    }
}