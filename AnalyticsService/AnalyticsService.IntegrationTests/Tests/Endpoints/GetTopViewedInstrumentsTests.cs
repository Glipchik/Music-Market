using System.Net;
using System.Net.Http.Json;
using AnalyticsService.Business.Models;
using AnalyticsService.DataAccess.Entities;
using AnalyticsService.IntegrationTests.Builders;
using FluentAssertions;

namespace AnalyticsService.IntegrationTests.Tests.Endpoints;

[Collection("AnalyticsTests")]
public class GetTopViewedInstrumentsTests(CustomWebApplicationFactory factory) : AnalyticsTestBase(factory)
{
    [Fact]
    public async Task GetTopViewedInstruments_ShouldReturnSortedLimitedResult_WhenStatsExist()
    {
        // Arrange
        var stats = new List<InstrumentStat>
        {
            new InstrumentStatBuilder().WithId("i1").WithViews(20).WithContactViews(5).WithBookmarks(1).Build(),
            new InstrumentStatBuilder().WithId("i2").WithViews(40).WithContactViews(2).WithBookmarks(3).Build(),
            new InstrumentStatBuilder().WithId("i3").WithViews(30).WithContactViews(7).WithBookmarks(4).Build(),
            new InstrumentStatBuilder().WithId("i4").WithViews(35).WithContactViews(13).WithBookmarks(2).Build()
        };

        await UnitOfWork.InstrumentStatRepository.AddRangeAsync(stats, CancellationToken.None);
        await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var response = await Client.GetAsync("/analytics/instruments/top?limit=3");
        var content = await response.Content.ReadFromJsonAsync<List<TopInstrumentModel>>();

        // Assert
        content.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        content.Should()
            .HaveCount(3)
            .And.SatisfyRespectively(
                first => first.InstrumentId.Should().Be("i2"),
                second => second.InstrumentId.Should().Be("i4"),
                third => third.InstrumentId.Should().Be("i3"));
    }

    [Fact]
    public async Task GetTopViewedInstruments_ShouldReturnEmptyList_WhenNoDataExists()
    {
        // Act
        var response = await Client.GetAsync("/analytics/instruments/top?limit=5");
        var content = await response.Content.ReadFromJsonAsync<List<TopInstrumentModel>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeNull();
        content.Should().BeEmpty();
    }
}