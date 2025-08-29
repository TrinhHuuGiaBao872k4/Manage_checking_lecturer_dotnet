using MongoElearn.Models;
using MongoDB.Driver;
namespace MongoElearn.Infrastructure.Repositories;

public interface IGiangVienLopHocRepository : IRepository<GiangVien_LopHoc>
{
    Task<GiangVien_LopHoc?> GetByIdAsync(int id);
    Task<List<GiangVien_LopHoc>> GetByGiangVienAsync(int gvId);
    Task<List<GiangVien_LopHoc>> GetByLopHocAsync(int lopId);
}
public class GiangVienLopHocRepository : MongoRepository<GiangVien_LopHoc>, IGiangVienLopHocRepository
{
    public GiangVienLopHocRepository(IMongoCollection<GiangVien_LopHoc> col) : base(col) { }

    public Task<GiangVien_LopHoc?> GetByIdAsync(int id)
        => FirstOrDefaultAsync(Builders<GiangVien_LopHoc>.Filter.Eq(x => x.GiangVien_LopHoc_Id, id));

    public Task<List<GiangVien_LopHoc>> GetByGiangVienAsync(int gvId)
        => FindAsync(Builders<GiangVien_LopHoc>.Filter.Eq(x => x.GiangVien_Id, gvId));

    public Task<List<GiangVien_LopHoc>> GetByLopHocAsync(int lopId)
        => FindAsync(Builders<GiangVien_LopHoc>.Filter.Eq(x => x.LopHoc_Id, lopId));
}
