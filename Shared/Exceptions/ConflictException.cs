namespace Shared.Exceptions;

public class ConflictException(string message = "Conflict") : Exception(message);