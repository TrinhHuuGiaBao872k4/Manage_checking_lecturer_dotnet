using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetByIdAsync(string id);
    Task<Project?> GetByCodeAsync(string code);
}
public class ProjectRepository : MongoRepository<Project>, IProjectRepository
{
    public ProjectRepository(IMongoCollection<Project> col) : base(col) { }

    public Task<Project?> GetByIdAsync(string id)
        => FirstOrDefaultAsync(Builders<Project>.Filter.Eq(x => x.id, id));

    public Task<Project?> GetByCodeAsync(string code)
        => FirstOrDefaultAsync(Builders<Project>.Filter.Eq(x => x.code, code));
}