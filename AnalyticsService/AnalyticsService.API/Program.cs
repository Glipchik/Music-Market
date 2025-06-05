using AnalyticsService.API.Middlewares;
using AnalyticsService.Business.Extensions;
using AnalyticsService.DataAccess.Extensions;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

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

app.MapControllers();

app.Run();