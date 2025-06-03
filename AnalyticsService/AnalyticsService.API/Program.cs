using AnalyticsService.API.Middlewares;
using AnalyticsService.Business.Extensions;
using AnalyticsService.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessServices();
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