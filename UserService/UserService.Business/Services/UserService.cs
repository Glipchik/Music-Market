using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using UserService.Business.Abstractions;
using UserService.Business.Common;
using UserService.Business.Models;
using UserService.DataAccess.Entities;

namespace UserService.Business.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<UserContactsModel> GetUserContactsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.UserNotFound(userId));
        }

        var userContactsModel = new UserContactsModel(user.Name, user.Email!);

        return userContactsModel;
    }
}