using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;

namespace MongoElearn.Services;

public class GiangVienLopHocService
{
    private readonly IMongoCollection<GiangVien_LopHoc> _col;

    public GiangVienLopHocService(MongoDbContext ctx)
        => _col = ctx.GiangVien_LopHoc;

    public Task<List<GiangVien_LopHoc>> GetAllAsync()
        => _col.Find(_ => true).ToListAsync();

    public Task<GiangVien_LopHoc?> GetByIdAsync(int id)
        => _col.Find(x => x.GiangVien_LopHoc_Id == id).FirstOrDefaultAsync();

    public Task CreateAsync(GiangVien_LopHoc doc)
        => _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(int id, GiangVien_LopHoc update)
    {
        update.GiangVien_LopHoc_Id = id;
        var result = await _col.ReplaceOneAsync(x => x.GiangVien_LopHoc_Id == id, update);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _col.DeleteOneAsync(x => x.GiangVien_LopHoc_Id == id);
        return result.DeletedCount > 0;
    }

    // Truy vấn tiện ích
    public Task<List<GiangVien_LopHoc>> GetByGiangVienAsync(int gvId)
        => _col.Find(x => x.GiangVien_Id == gvId).ToListAsync();

    public Task<List<GiangVien_LopHoc>> GetByLopHocAsync(int lopId)
        => _col.Find(x => x.LopHoc_Id == lopId).ToListAsync();
}
