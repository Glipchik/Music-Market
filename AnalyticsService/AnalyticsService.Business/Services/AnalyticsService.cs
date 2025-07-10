using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Models;
using AnalyticsService.DataAccess.Abstractions;
using AutoMapper;
using Shared.Exceptions;

namespace AnalyticsService.Business.Services;

public class AnalyticsService(IUnitOfWork unitOfWork, IMapper mapper) : IAnalyticsService
{
    public async Task<InstrumentStatResult> GetInstrumentStatAsync(string instrumentId,
        CancellationToken cancellationToken)
    {
        var instrumentStat = await unitOfWork.InstrumentStatRepository.GetByIdAsync(instrumentId, cancellationToken);

        if (instrumentStat is null)
        {
            throw new NotFoundException($"InstrumentStat with id {instrumentId} was not found");
        }

        var result = mapper.Map<InstrumentStatResult>(instrumentStat);

        return result;
    }

    public async Task<InstrumentDailyStatResult> GetInstrumentDailyStatByDateAsync(string instrumentId, DateOnly date,
        CancellationToken cancellationToken)
    {
        var instrumentDailyStat = await unitOfWork.InstrumentDailyStatRepository
            .GetByDateAsync(instrumentId, date, cancellationToken);

        if (instrumentDailyStat is null)
        {
            throw new NotFoundException(
                $"InstrumentDailyStat for instrumentId '{instrumentId}' on date '{date:yyyy-MM-dd}' was not found");
        }

        var result = mapper.Map<InstrumentDailyStatResult>(instrumentDailyStat);

        return result;
    }

    public async Task<List<InstrumentDailyStatResult>> GetInstrumentDailyStatsByDateRangeAsync(string instrumentId,
        DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var instrumentDailyStats = await unitOfWork.InstrumentDailyStatRepository
            .GetByDateRangeAsync(instrumentId, startDate, endDate, cancellationToken);

        var result = mapper.Map<List<InstrumentDailyStatResult>>(instrumentDailyStats);

        return result;
    }

    public async Task<UserStatResult> GetUserStatAsync(string userId, CancellationToken cancellationToken)
    {
        var userStat = await unitOfWork.UserStatRepository.GetByIdAsync(userId, cancellationToken);

        if (userStat is null)
        {
            throw new NotFoundException($"UserStat with id {userId} was not found");
        }

        var result = mapper.Map<UserStatResult>(userStat);

        return result;
    }

    public async Task<List<TopInstrumentModel>> GetTopViewedInstrumentIdsAsync(int limit,
        CancellationToken cancellationToken)
    {
        var instrumentStats = await unitOfWork.InstrumentStatRepository
            .GetTopViewedAsync(limit, cancellationToken);

        var topInstrumentModels = mapper.Map<List<TopInstrumentModel>>(instrumentStats);

        return topInstrumentModels;
    }
}