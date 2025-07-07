using DotNetEnv;
using InstrumentService.API.Extensions;
using InstrumentService.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddJwtAuthorization(builder.Configuration)
    .AddProjectServices(builder.Configuration)
    .AddRabbitMq(builder.Configuration);

builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.SeedInstrumentsAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProjectMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();