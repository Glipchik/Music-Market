using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Entities;
using InstrumentService.DataAccess.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InstrumentService.DataAccess.Repositories;

internal class InstrumentRepository(IMongoClient mongoClient, IOptions<InstrumentDbOptions> instrumentDbOptions)
    : Repository<Instrument>(mongoClient, instrumentDbOptions.Value.DatabaseName,
        instrumentDbOptions.Value.InstrumentsCollectionName), IInstrumentRepository
{
    public async Task<List<Instrument>> GetPagedByUserId(int skip, int take, string userId,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Instrument>.Filter.Eq(instrument => instrument.OwnerId, userId);
        var instruments = await Collection
            .Find(filter)
            .Skip(skip)
            .Limit(take)
            .ToListAsync(cancellationToken) ?? [];

        return instruments;
    }

    public async Task<int> CountByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<Instrument>.Filter.Eq(instrument => instrument.OwnerId, userId);
        var count = await Collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        return (int)count;
    }


    public async Task<List<Instrument>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken)
    {
        var instruments = await Collection
            .Find(_ => true)
            .Skip(skip)
            .Limit(take)
            .ToListAsync(cancellationToken) ?? [];

        return instruments;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        var count = await Collection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

        return (int)count;
    }

    public async Task<List<Instrument>> GetByIdRangeAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var filter = Builders<Instrument>.Filter.In(instrument => instrument.Id, ids);
        var instruments = await Collection.Find(filter).ToListAsync(cancellationToken);

        return instruments;
    }
}