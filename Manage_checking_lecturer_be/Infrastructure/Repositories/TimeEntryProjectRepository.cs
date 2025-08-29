using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure.Repositories;

public interface ITimeEntryProjectRepository : IRepository<TimeEntryProject>
{
    Task<TimeEntryProject?> GetByIdAsync(string id);
    Task<List<TimeEntryProject>> GetByEmployeeAsync(string employeeId);
    Task<List<TimeEntryProject>> GetByProjectAsync(string projectId);
}
public class TimeEntryProjectRepository : MongoRepository<TimeEntryProject>, ITimeEntryProjectRepository
{
    public TimeEntryProjectRepository(IMongoCollection<TimeEntryProject> col) : base(col) { }

    public Task<TimeEntryProject?> GetByIdAsync(string id)
        => FirstOrDefaultAsync(Builders<TimeEntryProject>.Filter.Eq(x => x.id, id));

    public Task<List<TimeEntryProject>> GetByEmployeeAsync(string employeeId)
        => FindAsync(Builders<TimeEntryProject>.Filter.Eq(x => x.employeeId, employeeId));

    public Task<List<TimeEntryProject>> GetByProjectAsync(string projectId)
        => FindAsync(Builders<TimeEntryProject>.Filter.Eq(x => x.projectId, projectId));
}