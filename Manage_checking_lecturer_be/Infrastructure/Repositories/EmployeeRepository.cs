// Infrastructure/Repositories/EmployeeRepository.cs
using MongoDB.Bson;
using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure.Repositories;
public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByIdAsync(string id);
    Task<Employee?> GetByEmailAsync(string email);
    Task<List<Employee>> SearchAsync(string? keyword, bool? isActive, string? role);
}


public class EmployeeRepository : MongoRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(IMongoCollection<Employee> col) : base(col) { }

    public Task<Employee?> GetByIdAsync(string id)
        => FirstOrDefaultAsync(Builders<Employee>.Filter.Eq(x => x.id, id));

    public Task<Employee?> GetByEmailAsync(string email)
        => FirstOrDefaultAsync(Builders<Employee>.Filter.Eq(x => x.email, email));

    public async Task<List<Employee>> SearchAsync(string? keyword, bool? isActive, string? role)
    {
        var f = Builders<Employee>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var rx = new BsonRegularExpression(keyword, "i");
            f &= Builders<Employee>.Filter.Or(
                Builders<Employee>.Filter.Regex(x => x.fullname!, rx),
                Builders<Employee>.Filter.Regex(e => e.email!, rx),
            Builders<Employee>.Filter.Regex(e => e.code!, rx)
            );
        }
        if (isActive.HasValue)
            f &= Builders<Employee>.Filter.Eq(x => x.isActive, isActive.Value);
        if (!string.IsNullOrWhiteSpace(role))
            f &= Builders<Employee>.Filter.AnyEq(x => x.role!, role);

        return await FindAsync(f);
    }
}
