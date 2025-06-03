using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Entities;
using AnalyticsService.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Repositories;

public class InstrumentDailyStatRepository(ApplicationDbContext context)
    : Repository<InstrumentDailyStat>(context), IInstrumentDailyStatRepository
{
    public async Task<List<InstrumentDailyStat>> GetByDateRangeAsync(Guid instrumentId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var instrumentDailyStats = await DbSet
            .Where(instrumentDailyStat => instrumentDailyStat.InstrumentId == instrumentId &&
                                          instrumentDailyStat.Date >= startDate &&
                                          instrumentDailyStat.Date <= endDate)
            .ToListAsync(cancellationToken);

        return instrumentDailyStats;
    }

    public async Task<InstrumentDailyStat?> GetByDateAsync(Guid instrumentId, DateOnly date,
        CancellationToken cancellationToken)
    {
        var instrumentDailyStat = await DbSet
            .FirstOrDefaultAsync(instrumentDailyStat =>
                instrumentDailyStat.InstrumentId == instrumentId &&
                instrumentDailyStat.Date == date, cancellationToken);

        return instrumentDailyStat;
    }

    public async Task DeleteByInstrumentIdAsync(Guid instrumentId, CancellationToken cancellationToken)
    {
        var instrumentDailyStats = await DbSet
            .Where(instrumentDailyStat => instrumentDailyStat.InstrumentId == instrumentId)
            .ToListAsync(cancellationToken);

        DbSet.RemoveRange(instrumentDailyStats);
    }
}