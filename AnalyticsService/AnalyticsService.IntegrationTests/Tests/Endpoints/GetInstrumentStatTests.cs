using System.Net;
using System.Net.Http.Json;
using AnalyticsService.Business.Models;
using AnalyticsService.IntegrationTests.Builders;
using AnalyticsService.IntegrationTests.Constants;
using FluentAssertions;

namespace AnalyticsService.IntegrationTests.Tests.Endpoints;

[Collection("AnalyticsTests")]
public class GetInstrumentStatTests(CustomWebApplicationFactory factory) : AnalyticsTestBase(factory)
{
    [Fact]
    public async Task GetInstrumentStat_ShouldReturnStat_WhenInstrumentExists()
    {
        // Arrange
        var stat = new InstrumentStatBuilder()
            .WithId("guitar-123")
            .WithViews(42)
            .WithContactViews(10)
            .WithBookmarks(3)
            .Build();

        var expected = new InstrumentStatResultExpectationBuilder()
            .FromEntity(stat)
            .Build();

        await UnitOfWork.InstrumentStatRepository.AddAsync(stat, default);
        await UnitOfWork.SaveChangesAsync(default);

        // Act
        var response = await Client.GetAsync($"/analytics/instruments/{stat.InstrumentId}/stats");
        var content = await response.Content.ReadFromJsonAsync<InstrumentStatResult>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetInstrumentStat_ShouldReturn404_WhenInstrumentNotExists()
    {
        // Act
        var response = await Client.GetAsync($"/analytics/instruments/{TestConstants.NonExistentInstrumentId}/stats");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}