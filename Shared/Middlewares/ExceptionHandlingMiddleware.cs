using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

namespace Shared.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = context.Response;
        var errorResponse = new ErrorResponse();

        response.StatusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        errorResponse.StatusCode = response.StatusCode;

        errorResponse.Message = response.StatusCode switch
        {
            StatusCodes.Status500InternalServerError => "An unexpected error occurred.",
            _ => exception.Message
        };

        if (response.StatusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception,
                "An unhandled exception occurred during request processing. Status Code: {StatusCode}",
                response.StatusCode);
        }

        var json = JsonSerializer.Serialize(errorResponse);

        await response.WriteAsync(json);
    }

    private class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
    }
}