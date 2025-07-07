using InstrumentService.DataAccess.Entities;

namespace InstrumentService.DataAccess.Abstractions;

public interface IInstrumentRepository : IRepository<Instrument>
{
    Task<List<Instrument>> GetPagedByUserId(int skip, int take, string userId, CancellationToken cancellationToken);
    Task<int> CountByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<List<Instrument>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<List<Instrument>> GetByIdRangeAsync(List<string> ids, CancellationToken cancellationToken);
}