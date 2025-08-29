using MongoDB.Driver;
using MongoElearn.Models;
using MongoElearn.Infrastructure;

namespace MongoElearn.Services;

public class EmployeeService
{
    private readonly IMongoCollection<Employee> _col;

    public EmployeeService(MongoDbContext ctx)
    {
        _col = ctx.Employee;
    }

    public Task<List<Employee>> GetAllAsync() =>
        _col.Find(_ => true).ToListAsync();

    public Task<Employee?> GetByIdAsync(string id) =>
        _col.Find(x => x.id == id).FirstOrDefaultAsync();

    public Task CreateAsync(Employee doc) =>
        _col.InsertOneAsync(doc);

    public async Task<bool> UpdateAsync(string id, Employee update)
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
