using Microsoft.Extensions.DependencyInjection;
using UserService.Business.Abstractions;
using UserService.Business.Services;

namespace UserService.Business.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, Services.UserService>();
        
        return services;
    }
}