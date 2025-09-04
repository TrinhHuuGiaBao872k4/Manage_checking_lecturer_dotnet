using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure.Repositories;

public interface ILopHocRepository : IRepository<LopHoc>
{
    Task<LopHoc?> GetByIdAsync(int id);
}
public class LopHocRepository : MongoRepository<LopHoc>, ILopHocRepository
{
    public LopHocRepository(IMongoCollection<LopHoc> col) : base(col) { }
    public Task<LopHoc?> GetByIdAsync(int id)
        => FirstOrDefaultAsync(Builders<LopHoc>.Filter.Eq(x => x.LopHoc_Id, id));
}