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
public class InstrumentViewedConsumerTests(CustomWebApplicationFactory factory) : AnalyticsTestBase(factory)
{
    private InstrumentViewedConsumer _consumer = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _consumer = Scope.ServiceProvider.GetRequiredService<InstrumentViewedConsumer>();
    }

    [Fact]
    public async Task Consume_ShouldCreateStatAndDailyStat_WhenNoneExist()
    {
        // Arrange
        var instrumentId = TestConstants.InstrumentId;
        var date = new DateOnly(2024, 1, 1);

        var message = new InstrumentViewed(instrumentId, date);
        var consumeContext = new TestConsumeContext<InstrumentViewed>(message);

        // Act
        await _consumer.Consume(consumeContext);
        var statResponse = await Client.GetAsync($"/analytics/instruments/{instrumentId}/stats");
        var stat = await statResponse.Content.ReadFromJsonAsync<InstrumentStatResult>();

        var dailyStatResponse = await Client.GetAsync($"/analytics/instruments/{instrumentId}/daily-stats?date={date}");
        var dailyStat = await dailyStatResponse.Content.ReadFromJsonAsync<InstrumentDailyStatResult>();

        // Assert
        stat.Should().NotBeNull();
        stat.Views.Should().Be(1);
        dailyStat.Should().NotBeNull();
        dailyStat.Views.Should().Be(1);
    }

    [Fact]
    public async Task Consume_ShouldIncrementViews_WhenBothExist()
    {
        // Arrange
        var instrumentId = TestConstants.InstrumentId;
        var date = TestConstants.GetDate();

        var message = new InstrumentViewed(instrumentId, date);
        var consumeContext = new TestConsumeContext<InstrumentViewed>(message);

        var existingStat = new InstrumentStatBuilder()
            .WithId(TestConstants.InstrumentId)
            .WithViews(10)
            .Build();

        var existingDailyStat = new InstrumentDailyStatBuilder()
            .WithId(TestConstants.InstrumentId)
            .WithViews(3)
            .WithDate(TestConstants.GetDate())
            .Build();

        await UnitOfWork.InstrumentStatRepository.AddAsync(existingStat, default);
        await UnitOfWork.InstrumentDailyStatRepository.AddAsync(existingDailyStat, default);
        await UnitOfWork.SaveChangesAsync(default);

        // Act
        await _consumer.Consume(consumeContext);
        var statResponse = await Client.GetAsync($"/analytics/instruments/{instrumentId}/stats");
        var stat = await statResponse.Content.ReadFromJsonAsync<InstrumentStatResult>();

        var dailyStatResponse = await Client.GetAsync($"/analytics/instruments/{instrumentId}/daily-stats?date={date}");
        var dailyStat = await dailyStatResponse.Content.ReadFromJsonAsync<InstrumentDailyStatResult>();

        // Assert
        stat.Should().NotBeNull();
        stat.Views.Should().Be(11);

        dailyStat.Should().NotBeNull();
        dailyStat.Views.Should().Be(4);
    }
}