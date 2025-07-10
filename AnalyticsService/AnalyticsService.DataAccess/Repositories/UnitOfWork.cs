using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;

namespace AnalyticsService.DataAccess.Repositories;

internal class UnitOfWork(
    ApplicationDbContext context,
    IInstrumentDailyStatRepository instrumentDailyStatRepository,
    IInstrumentStatRepository instrumentStatRepository,
    IUserStatRepository userStatRepository) : IUnitOfWork
{
    public IInstrumentDailyStatRepository InstrumentDailyStatRepository => instrumentDailyStatRepository;
    public IInstrumentStatRepository InstrumentStatRepository => instrumentStatRepository;
    public IUserStatRepository UserStatRepository => userStatRepository;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new Exception("An error occured while saving data to AnalyticsService database.", exception);
        }
    }
}