using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using Shared.Exceptions;

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
        catch (Exception)
        {
            throw new DataSaveException("An error occured while saving data");
        }
    }
}