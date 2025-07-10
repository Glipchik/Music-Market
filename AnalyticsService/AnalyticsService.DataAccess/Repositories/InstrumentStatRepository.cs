using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Repositories;

internal class InstrumentStatRepository(ApplicationDbContext context)
    : Repository<InstrumentStat, string>(context), IInstrumentStatRepository
{
    public async Task DeleteByInstrumentIdAsync(string instrumentId, CancellationToken cancellationToken)
    {
        var instrumentStat = await DbSet
            .FirstOrDefaultAsync(instrumentStat =>
                instrumentStat.InstrumentId == instrumentId, cancellationToken);

        if (instrumentStat is not null)
        {
            DbSet.Remove(instrumentStat);
        }
    }

    public async Task<List<InstrumentStat>> GetTopViewedAsync(int limit, CancellationToken cancellationToken)
    {
        var instrumentStats = await DbSet
            .OrderByDescending(instrumentStat => instrumentStat.Views)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return instrumentStats;
    }
}