using InstrumentService.DataAccess.Entities;

namespace InstrumentService.DataAccess.Abstractions;

public interface IInstrumentRepository : IRepository<Instrument>
{
    Task<List<Instrument>> GetByUserId(string userId, CancellationToken cancellationToken);
    Task<List<Instrument>> GetByIdRangeAsync(List<string> ids, CancellationToken cancellationToken);
}