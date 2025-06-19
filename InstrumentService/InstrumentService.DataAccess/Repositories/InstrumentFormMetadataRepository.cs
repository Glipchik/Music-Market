using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Entities;
using InstrumentService.DataAccess.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InstrumentService.DataAccess.Repositories;

internal class InstrumentFormMetadataRepository(
    IMongoClient mongoClient,
    IOptions<InstrumentDbOptions> instrumentFormMetadataOptions)
    : Repository<InstrumentFormMetadata>(mongoClient, instrumentFormMetadataOptions.Value.DatabaseName,
        instrumentFormMetadataOptions.Value.InstrumentFormMetadataCollectionName), IInstrumentFormMetadataRepository
{
    public new async Task<InstrumentFormMetadata?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<InstrumentFormMetadata>.Filter.Eq(field => field.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpsertAsync(InstrumentFormMetadata metadata, CancellationToken cancellationToken)
    {
        var filter = Builders<InstrumentFormMetadata>.Filter
            .Eq(field => field.Id, metadata.Id);

        await Collection.ReplaceOneAsync(
            filter,
            metadata,
            new ReplaceOptions { IsUpsert = true },
            cancellationToken);
    }
}