using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.API.Controllers;

[ApiController]
[Route("analytics")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("instruments/{instrumentId:guid}/stats")]
    public async Task<InstrumentStatResult> GetInstrumentStat(Guid instrumentId, CancellationToken cancellationToken)
    {
        var response = await analyticsService
            .GetInstrumentStatAsync(instrumentId, cancellationToken);

        return response;
    }
    
    [HttpGet("instruments/{instrumentId:guid}/daily-stats")]
    public async Task<InstrumentDailyStatResult> GetInstrumentDailyStat(Guid instrumentId, [FromQuery] DateOnly date,
        CancellationToken cancellationToken)
    {
        var dateOnly = date;
        var response = await analyticsService
            .GetInstrumentDailyStatByDateAsync(instrumentId, date, cancellationToken);
        
        return response;
    }

    [HttpGet("instruments/{instrumentId}/stats-range")]
    public async Task<List<InstrumentDailyStatResult>> GetInstrumentDailyStatsForRange(
        Guid instrumentId,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var response = await analyticsService
            .GetInstrumentDailyStatsByDateRangeAsync(instrumentId, startDate, endDate, cancellationToken);
        
        return response;
    }

    [HttpGet("users/{userId}/stats")]
    public async Task<UserStatResult> GetUserStats(Guid userId, CancellationToken cancellationToken)
    {
        var response = await analyticsService.GetUserStatAsync(userId, cancellationToken);
        
        return response;
    }
}