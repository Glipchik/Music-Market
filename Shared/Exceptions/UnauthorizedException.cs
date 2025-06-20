namespace Shared.Exceptions;

public class UnauthorizedException(string message = "Unauthorized")
    : Exception(message);