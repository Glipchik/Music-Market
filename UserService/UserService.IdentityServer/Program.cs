using DotNetEnv;
using UserService.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Seeder.SeedRolesAsync(services);
    await Seeder.SeedAdminUserAsync(services);
}

app.Run();