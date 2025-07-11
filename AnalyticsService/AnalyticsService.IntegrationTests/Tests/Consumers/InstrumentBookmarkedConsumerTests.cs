using System.Net;
using System.Net.Http.Json;
using AnalyticsService.Business.Consumers.Instrument;
using AnalyticsService.Business.Models;
using AnalyticsService.IntegrationTests.Builders;
using AnalyticsService.IntegrationTests.Constants;
using FluentAssertions;
using MassTransit.TestFramework;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Contracts.Events.Instrument;

namespace AnalyticsService.IntegrationTests.Tests.Consumers;

[Collection("AnalyticsTests")]
public class InstrumentBookmarkedConsumerTests(CustomWebApplicationFactory factory) : AnalyticsTestBase(factory)
{
    private InstrumentBookmarkedConsumer _consumer = null!;
    private IServiceScope _scope = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _scope = Factory.Services.CreateScope();
        _consumer = _scope.ServiceProvider.GetRequiredService<InstrumentBookmarkedConsumer>();
    }

    public override async Task DisposeAsync()
    {
        await base.DisposeAsync();
        _scope.Dispose();
    }

    [Fact]
    public async Task Consume_ShouldCreateStat_WhenInstrumentDoesNotExist()
    {
        // Arrange
        var instrumentId = TestConstants.InstrumentId;
        var message = new InstrumentBookmarked(instrumentId);
        var consumerContext = new TestConsumeContext<InstrumentBookmarked>(message);

        // Act
        await _consumer.Consume(consumerContext);

        var response = await Client.GetAsync($"/analytics/instruments/{instrumentId}/stats");
        var content = await response.Content.ReadFromJsonAsync<InstrumentStatResult>();

        // Assert
        content.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Bookmarks.Should().Be(1);
        content.InstrumentId.Should().Be(instrumentId);
    }

    [Fact]
    public async Task Consume_ShouldIncrementBookmarks_WhenInstrumentExists()
    {
        // Arrange
        var instrumentId = TestConstants.InstrumentId;

        var existingStat = new InstrumentStatBuilder()
            .WithId(instrumentId)
            .WithBookmarks(2)
            .Build();

        await UnitOfWork.InstrumentStatRepository.AddAsync(existingStat, default);
        await UnitOfWork.SaveChangesAsync(default);

        var message = new InstrumentBookmarked(instrumentId);
        var consumerContext = new TestConsumeContext<InstrumentBookmarked>(message);

        // Act
        await _consumer.Consume(consumerContext);
        var response = await Client.GetAsync($"/analytics/instruments/{instrumentId}/stats");
        var content = await response.Content.ReadFromJsonAsync<InstrumentStatResult>();

        // Assert
        content.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Bookmarks.Should().Be(3);
    }
}