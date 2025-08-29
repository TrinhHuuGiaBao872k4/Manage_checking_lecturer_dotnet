
using MongoDB.Driver;

namespace MongoElearn.Infrastructure.Repositories;

public interface IRepository<T>
{
    IMongoCollection<T> Collection { get; }

    Task<List<T>> GetAllAsync();
    Task<List<T>> FindAsync(FilterDefinition<T> filter);
    Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter);

    Task CreateAsync(T entity);
    Task<bool> ReplaceAsync(FilterDefinition<T> filter, T entity);
    Task<bool> DeleteAsync(FilterDefinition<T> filter);
}
