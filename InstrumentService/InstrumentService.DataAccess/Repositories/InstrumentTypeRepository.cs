using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Entities;
using InstrumentService.DataAccess.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InstrumentService.DataAccess.Repositories;

internal class InstrumentTypeRepository(IMongoClient mongoClient, IOptions<InstrumentDbOptions> instrumentDbOptions)
    : Repository<InstrumentType>(mongoClient, instrumentDbOptions.Value.DatabaseName,
        instrumentDbOptions.Value.InstrumentTypesCollectionName), IInstrumentTypeRepository
{
    public new async Task<InstrumentType?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<InstrumentType>.Filter.Eq(field => field.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
    public async Task UpsertAsync(List<InstrumentType> instrumentTypes, CancellationToken cancellationToken)
    {
        foreach (var instrumentType in instrumentTypes)
        {
            var filter = Builders<InstrumentType>.Filter.Eq(field => field.Id, instrumentType.Id);

            await Collection.ReplaceOneAsync(
                filter,
                instrumentType,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken
            );
        }
    }
}