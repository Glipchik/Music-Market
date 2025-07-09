using Shared.Middlewares;

namespace InstrumentService.API.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder UseProjectMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}