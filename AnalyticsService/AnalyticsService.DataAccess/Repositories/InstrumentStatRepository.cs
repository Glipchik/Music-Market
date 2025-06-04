using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Repositories;

public class InstrumentStatRepository(ApplicationDbContext context)
    : Repository<InstrumentStat>(context), IInstrumentStatRepository
{
    public async Task DeleteByInstrumentIdAsync(Guid instrumentId, CancellationToken cancellationToken)
    {
        var instrumentStat = await DbSet
            .FirstOrDefaultAsync(instrumentStat =>
                instrumentStat.InstrumentId == instrumentId, cancellationToken);

        if (instrumentStat is not null)
        {
            DbSet.Remove(instrumentStat);
        }
    }
}