using InstrumentService.DataAccess.Clients.User.Models;

namespace InstrumentService.DataAccess.Abstractions;

public interface IUserClient
{
    Task<UserContacts> GetUserContactsAsync(string userId, CancellationToken cancellationToken);
}