namespace InstrumentService.DataAccess.Constants;

public static class UserRoutes
{
    public static string GetUserContacts(string userId) => $"/users/{userId}/contacts";
}