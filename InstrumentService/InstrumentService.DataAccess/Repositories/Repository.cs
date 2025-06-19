using InstrumentService.DataAccess.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InstrumentService.DataAccess.Repositories;

internal class Repository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> Collection;
    protected Repository(IMongoClient client, string databaseName, string collectionName)
    {
        var database = client.GetDatabase(databaseName);
        Collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Collection.Find(_ => true).ToListAsync(cancellationToken) ?? [];
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
    }

    public async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        await Collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }
}