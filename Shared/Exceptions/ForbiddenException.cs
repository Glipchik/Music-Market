namespace Shared.Exceptions;

public class ForbiddenException(string message = "Forbidden") : Exception(message);