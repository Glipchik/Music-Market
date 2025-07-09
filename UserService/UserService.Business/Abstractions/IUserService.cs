using UserService.Business.Models;

namespace UserService.Business.Abstractions;

public interface IUserService
{
    Task<UserContactsModel> GetUserContactsAsync(string userId, CancellationToken cancellationToken);
}