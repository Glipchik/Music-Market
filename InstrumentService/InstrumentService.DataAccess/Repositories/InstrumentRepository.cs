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
    public async Task<List<Instrument>> GetByUserId(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<Instrument>.Filter.Eq(instrument => instrument.OwnerId, userId);
        var instruments = await Collection.Find(filter).ToListAsync(cancellationToken) ?? [];

        return instruments;
    }

    public async Task<List<Instrument>> GetByIdRangeAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var filter = Builders<Instrument>.Filter.In(instrument => instrument.Id, ids);
        var instruments = await Collection.Find(filter).ToListAsync(cancellationToken);
        
        return instruments;
    }
}