using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;

namespace MongoElearn.Services;

public class TimeEntryProjectService
{
    private readonly IMongoCollection<TimeEntryProject> _col;

    public TimeEntryProjectService(MongoDbContext ctx)
        => _col = ctx.TimeEntryProject;

    public Task<List<TimeEntryProject>> GetAllAsync()
        => _col.Find(_ => true).ToListAsync();

    public Task<TimeEntryProject?> GetByIdAsync(string id)
        => _col.Find(x => x.id == id).FirstOrDefaultAsync();

    public Task CreateAsync(TimeEntryProject doc)
        => _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(string id, TimeEntryProject update)
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

    // Một số truy vấn tiện ích
    public Task<List<TimeEntryProject>> GetByEmployeeAsync(string employeeId)
        => _col.Find(x => x.employeeId == employeeId).ToListAsync();

    public Task<List<TimeEntryProject>> GetByProjectAsync(string projectId)
        => _col.Find(x => x.projectId == projectId).ToListAsync();
}
