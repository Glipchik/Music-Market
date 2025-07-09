namespace UserService.Business.Common;

public static class ErrorMessages
{
    public static string UserNotFound(string userId) =>
        $"User with ID '{userId}' was not found.";
}