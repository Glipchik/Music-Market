using InstrumentService.DataAccess.Entities;
using InstrumentService.IntegrationTests.Constants;
using InstrumentService.IntegrationTests.Extensions;
using MongoDB.Driver;

namespace InstrumentService.IntegrationTests;

public abstract class InstrumentTestBase(CustomWebApplicationFactory factory) : IAsyncLifetime
{
    protected readonly HttpClient Client = factory.CreateAuthenticatedClient(AuthTestConstants.UserId);
    protected readonly CustomWebApplicationFactory Factory = factory;

    protected readonly IMongoCollection<Instrument> InstrumentsCollection =
        factory.GetCollection<Instrument>("Instruments");

    public virtual async Task InitializeAsync() => await Factory.ClearCollectionsAsync("Instruments");
    public virtual Task DisposeAsync() => Task.CompletedTask;
}