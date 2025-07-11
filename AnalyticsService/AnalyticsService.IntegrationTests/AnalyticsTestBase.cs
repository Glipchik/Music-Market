using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.IntegrationTests.Constants;
using AnalyticsService.IntegrationTests.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.IntegrationTests;

public abstract class AnalyticsTestBase(CustomWebApplicationFactory factory) : IAsyncLifetime
{
    protected readonly HttpClient Client = factory.CreateAuthenticatedClient(TestAuthConstants.UserId);
    protected readonly CustomWebApplicationFactory Factory = factory;

    private IServiceScope _scope = null!;
    protected IUnitOfWork UnitOfWork = null!;

    public virtual async Task InitializeAsync()
    {
        await Factory.ResetDatabaseAsync();

        _scope = Factory.Services.CreateScope();
        UnitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    public virtual Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}