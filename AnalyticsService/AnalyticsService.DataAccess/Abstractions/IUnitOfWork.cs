namespace AnalyticsService.DataAccess.Abstractions;

public interface IUnitOfWork
{
    IInstrumentDailyStatRepository InstrumentDailyStatRepository { get; }
    IInstrumentStatRepository InstrumentStatRepository { get; }
    IUserStatRepository UserStatRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}