using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.API.Controllers;

[ApiController]
[Route("analytics")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("instruments/{instrumentId}/stats")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<InstrumentStatResult> GetInstrumentStat(string instrumentId, CancellationToken cancellationToken)
    {
        var response = await analyticsService
            .GetInstrumentStatAsync(instrumentId, cancellationToken);

        return response;
    }

    [HttpGet("instruments/{instrumentId}/daily-stats")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<InstrumentDailyStatResult> GetInstrumentDailyStat(string instrumentId, [FromQuery] DateOnly date,
        CancellationToken cancellationToken)
    {
        var response = await analyticsService
            .GetInstrumentDailyStatByDateAsync(instrumentId, date, cancellationToken);

        return response;
    }

    [HttpGet("instruments/{instrumentId}/stats-range")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<List<InstrumentDailyStatResult>> GetInstrumentDailyStatsForDateRange(
        string instrumentId,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var response = await analyticsService
            .GetInstrumentDailyStatsByDateRangeAsync(instrumentId, startDate, endDate, cancellationToken);

        return response;
    }

    [HttpGet("users/{userId}/stats")]
    public async Task<UserStatResult> GetUserStats(string userId, CancellationToken cancellationToken)
    {
        var response = await analyticsService.GetUserStatAsync(userId, cancellationToken);

        return response;
    }

    [HttpGet("instruments/top")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<List<TopInstrumentModel>> GetTopViewedInstruments([FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var response = await analyticsService.GetTopViewedInstrumentIdsAsync(limit, cancellationToken);

        return response;
    }
}