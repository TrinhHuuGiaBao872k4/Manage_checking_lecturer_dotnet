using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;

namespace MongoElearn.Services;

public class ProjectService
{
    private readonly IMongoCollection<Project> _col;

    public ProjectService(MongoDbContext ctx)
        => _col = ctx.Project;

    public Task<List<Project>> GetAllAsync()
        => _col.Find(_ => true).ToListAsync();

    public Task<Project?> GetByIdAsync(string id)
        => _col.Find(x => x.id == id).FirstOrDefaultAsync();

    public Task CreateAsync(Project doc)
        => _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(string id, Project update)
    {
        update.id = id;
        var result = await _col.ReplaceOneAsync(x => x.id == id, update);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _col.DeleteOneAsync(x => x.id == id);
        return result.DeletedCount > 0;
    }
}
