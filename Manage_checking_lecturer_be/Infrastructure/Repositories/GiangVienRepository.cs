using MongoElearn.Models;
using MongoDB.Driver;
namespace MongoElearn.Infrastructure.Repositories;

public interface IGiangVienRepository : IRepository<GiangVien>
{
    Task<GiangVien?> GetByIdAsync(int id);
}
public class GiangVienRepository : MongoRepository<GiangVien>, IGiangVienRepository
{
    public GiangVienRepository(IMongoCollection<GiangVien> col) : base(col) { }
    public Task<GiangVien?> GetByIdAsync(int id)
        => FirstOrDefaultAsync(Builders<GiangVien>.Filter.Eq(x => x.id, id));
}