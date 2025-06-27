using InstrumentService.DataAccess.Entities;

namespace InstrumentService.DataAccess.Abstractions;

public interface IInstrumentTypeRepository : IRepository<InstrumentType>
{
    new Task<InstrumentType?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task UpsertAsync(List<InstrumentType> instrumentType, CancellationToken cancellationToken);
}