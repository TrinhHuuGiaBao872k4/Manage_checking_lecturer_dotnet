using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;

namespace MongoElearn.Services;

public class LopHocService
{
    private readonly IMongoCollection<LopHoc> _col;

    public LopHocService(MongoDbContext ctx)
        => _col = ctx.LopHoc;

    public Task<List<LopHoc>> GetAllAsync()
        => _col.Find(_ => true).ToListAsync();

    public Task<LopHoc?> GetByIdAsync(int id)
        => _col.Find(x => x.LopHoc_Id == id).FirstOrDefaultAsync();

    public Task CreateAsync(LopHoc doc)
        => _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(int id, LopHoc update)
    {
        update.LopHoc_Id = id;
        var result = await _col.ReplaceOneAsync(x => x.LopHoc_Id == id, update);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _col.DeleteOneAsync(x => x.LopHoc_Id == id);
        return result.DeletedCount > 0;
    }
}