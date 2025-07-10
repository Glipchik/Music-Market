using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Repositories;

internal class InstrumentDailyStatRepository(ApplicationDbContext context)
    : Repository<InstrumentDailyStat, string>(context), IInstrumentDailyStatRepository
{
    public async Task<List<InstrumentDailyStat>> GetByDateRangeAsync(string instrumentId, DateOnly startDate,
        DateOnly endDate, CancellationToken cancellationToken)
    {
        var instrumentDailyStats = await DbSet
            .Where(instrumentDailyStat => instrumentDailyStat.InstrumentId == instrumentId &&
                                          instrumentDailyStat.Date >= startDate &&
                                          instrumentDailyStat.Date <= endDate)
            .ToListAsync(cancellationToken);

        return instrumentDailyStats;
    }

    public async Task<InstrumentDailyStat?> GetByDateAsync(string instrumentId, DateOnly date,
        CancellationToken cancellationToken)
    {
        var instrumentDailyStat = await DbSet
            .FirstOrDefaultAsync(instrumentDailyStat =>
                instrumentDailyStat.InstrumentId == instrumentId &&
                instrumentDailyStat.Date == date, cancellationToken);

        return instrumentDailyStat;
    }

    public async Task DeleteByInstrumentIdAsync(string instrumentId, CancellationToken cancellationToken)
    {
        var instrumentDailyStats = await DbSet
            .Where(instrumentDailyStat => instrumentDailyStat.InstrumentId == instrumentId)
            .ToListAsync(cancellationToken);

        DbSet.RemoveRange(instrumentDailyStats);
    }
}