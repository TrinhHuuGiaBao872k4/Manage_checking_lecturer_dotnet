using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;

namespace MongoElearn.Services;

public class RoleService
{
    private readonly IMongoCollection<role> _col;

    public RoleService(MongoDbContext ctx)
        => _col = ctx.role;

    public Task<List<role>> GetAllAsync()
        => _col.Find(_ => true).ToListAsync();

    public Task<role?> GetByIdAsync(int id)
        => _col.Find(x => x.id == id).FirstOrDefaultAsync();

    public Task CreateAsync(role doc)
        => _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(int id, role update)
    {
        update.id = id;
        var result = await _col.ReplaceOneAsync(x => x.id == id, update);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _col.DeleteOneAsync(x => x.id == id);
        return result.DeletedCount > 0;
    }
}
