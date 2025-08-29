
using MongoDB.Driver;

namespace MongoElearn.Infrastructure.Repositories;

public class MongoRepository<T> : IRepository<T>
{
    public IMongoCollection<T> Collection { get; }

    public MongoRepository(IMongoCollection<T> collection)
        => Collection = collection;

    public Task<List<T>> GetAllAsync()
        => Collection.Find(FilterDefinition<T>.Empty).ToListAsync();

    public Task<List<T>> FindAsync(FilterDefinition<T> filter)
        => Collection.Find(filter).ToListAsync();

    public Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter)
        => Collection.Find(filter).FirstOrDefaultAsync();

    public Task CreateAsync(T entity)
        => Collection.InsertOneAsync(entity);

    public async Task<bool> ReplaceAsync(FilterDefinition<T> filter, T entity)
    {
        var res = await Collection.ReplaceOneAsync(filter, entity);
        return res.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(FilterDefinition<T> filter)
    {
        var res = await Collection.DeleteOneAsync(filter);
        return res.DeletedCount > 0;
    }
}
