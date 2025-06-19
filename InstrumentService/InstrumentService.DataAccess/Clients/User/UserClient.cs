using System.Net.Http.Json;
using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Clients.User.Models;
using InstrumentService.DataAccess.Constants;

namespace InstrumentService.DataAccess.Clients.User;

public class UserClient(HttpClient httpClient) : IUserClient
{
    public async Task<UserContacts> GetUserContactsAsync(string userId, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(UserRoutes.GetUserContacts(userId), cancellationToken);

        return await response.Content.ReadFromJsonAsync<UserContacts>(cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize user contacts response.");
    }
}