using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;

namespace MongoElearn.Services;

public class GiangVienService
{
    private readonly IMongoCollection<GiangVien> _col;

    public GiangVienService(MongoDbContext ctx)
        => _col = ctx.GiangVien;

    public Task<List<GiangVien>> GetAllAsync()
        => _col.Find(_ => true).ToListAsync();

    public Task<GiangVien?> GetByIdAsync(int id)
        => _col.Find(x => x.id == id).FirstOrDefaultAsync();

    public Task CreateAsync(GiangVien doc)
        => _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(int id, GiangVien update)
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
