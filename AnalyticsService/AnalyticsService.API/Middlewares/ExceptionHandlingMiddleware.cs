using System.Net;
using Newtonsoft.Json;
using Shared.Exceptions;

namespace AnalyticsService.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = context.Response;
        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                errorResponse.Message = exception.Message;
                break;
            case DataSaveException:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Message = exception.Message;
                break;
            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Message = "An unexpected error occurred.";
                break;
        }

        errorResponse.StatusCode = response.StatusCode;

        var json = JsonConvert.SerializeObject(errorResponse);

        await response.WriteAsync(json);
    }

    private class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
    }
}