using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.IntegrationTests.Constants;
using AnalyticsService.IntegrationTests.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.IntegrationTests;

public abstract class AnalyticsTestBase(CustomWebApplicationFactory factory) : IAsyncLifetime
{
    protected readonly HttpClient Client = factory.CreateAuthenticatedClient(TestAuthConstants.UserId);
    protected readonly CustomWebApplicationFactory Factory = factory;

    protected IServiceScope Scope = null!;
    protected IUnitOfWork UnitOfWork = null!;

    public virtual async Task InitializeAsync()
    {
        await Factory.ResetDatabaseAsync();

        Scope = Factory.Services.CreateScope();
        UnitOfWork = Scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    public virtual Task DisposeAsync()
    {
        Scope.Dispose();
        return Task.CompletedTask;
    }
}