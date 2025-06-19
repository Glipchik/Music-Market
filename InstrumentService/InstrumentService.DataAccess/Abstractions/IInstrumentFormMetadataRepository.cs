using InstrumentService.DataAccess.Entities;

namespace InstrumentService.DataAccess.Abstractions;

public interface IInstrumentFormMetadataRepository : IRepository<InstrumentFormMetadata>
{
    new Task<InstrumentFormMetadata?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task UpsertAsync(InstrumentFormMetadata metadata, CancellationToken cancellationToken);
}