using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Entities;
using AnalyticsService.Business.Models;
using AutoMapper;
using Shared.Exceptions;

namespace AnalyticsService.Business.Services;

public class AnalyticsService(IUnitOfWork unitOfWork, IMapper mapper) : IAnalyticsService
{
    public async Task<InstrumentStatResult> GetInstrumentStatAsync(Guid instrumentId,
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

    public async Task<InstrumentDailyStatResult> GetInstrumentDailyStatByDateAsync(Guid instrumentId, DateOnly date,
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

    public async Task<List<InstrumentDailyStatResult>> GetInstrumentDailyStatsByDateRangeAsync(Guid instrumentId,
        DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var instrumentDailyStats = await unitOfWork.InstrumentDailyStatRepository
            .GetByDateRangeAsync(instrumentId, startDate, endDate, cancellationToken);

        var result = mapper.Map<List<InstrumentDailyStatResult>>(instrumentDailyStats);

        return result;
    }

    public async Task<UserStatResult> GetUserStatAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userStat = await unitOfWork.UserStatRepository.GetByIdAsync(userId, cancellationToken);

        if (userStat is null)
        {
            throw new NotFoundException($"UserStat with id {userId} was not found");
        }

        var result = mapper.Map<UserStatResult>(userStat);

        return result;
    }
}