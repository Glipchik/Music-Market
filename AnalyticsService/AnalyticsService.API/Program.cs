using AnalyticsService.API.Extensions;
using AnalyticsService.Business.Extensions;
using AnalyticsService.DataAccess.Extensions;
using DotNetEnv;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddJwtAuthorization(builder.Configuration);

builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddDataAccessServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();