namespace InstrumentService.IntegrationTests;

[CollectionDefinition("InstrumentTests")]
public class SharedTestCollection : ICollectionFixture<CustomWebApplicationFactory>;