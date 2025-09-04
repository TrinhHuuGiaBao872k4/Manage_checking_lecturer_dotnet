using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByIdAsync(int id);
}
public class RoleRepository : MongoRepository<Role>, IRoleRepository
{
    public RoleRepository(IMongoCollection<Role> col) : base(col) { }
    public Task<Role?> GetByIdAsync(int id)
        => FirstOrDefaultAsync(Builders<Role>.Filter.Eq(x => x.id, id));
}