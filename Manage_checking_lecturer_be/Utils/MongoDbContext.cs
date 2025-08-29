// Infrastructure/MongoDbContext.cs
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure;

public class MongoDbContext
{
    public IMongoDatabase Database { get; }
    private readonly MongoSettings _cfg;

    public MongoDbContext(IOptions<MongoSettings> options)
    {
        _cfg = options.Value;
        var client = new MongoClient(_cfg.ConnectionString);
        Database = client.GetDatabase(_cfg.DatabaseName);
    }

    public IMongoCollection<Employee> Employee =>
        Database.GetCollection<Employee>(_cfg.Collections.Employee);

    public IMongoCollection<GiangVien> GiangVien =>
        Database.GetCollection<GiangVien>(_cfg.Collections.GiangVien);

    public IMongoCollection<GiangVien_LopHoc> GiangVien_LopHoc =>
        Database.GetCollection<GiangVien_LopHoc>(_cfg.Collections.GiangVien_LopHoc);

    public IMongoCollection<LopHoc> LopHoc =>
        Database.GetCollection<LopHoc>(_cfg.Collections.LopHoc);

    public IMongoCollection<Project> Project =>
        Database.GetCollection<Project>(_cfg.Collections.Project);

    public IMongoCollection<Role> role =>
        Database.GetCollection<Role>(_cfg.Collections.Role);

    public IMongoCollection<TimeEntryProject> TimeEntryProject =>
        Database.GetCollection<TimeEntryProject>(_cfg.Collections.TimeEntryProject);

    // (tuỳ chọn) tạo index thay thế UNIQUE/FK
    public async Task EnsureIndexesAsync()
    {
        await Employee.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<Employee>(Builders<Employee>.IndexKeys.Ascending(x => x.email), new CreateIndexOptions{ Unique = true }),
            new CreateIndexModel<Employee>(Builders<Employee>.IndexKeys.Ascending(x => x.code),  new CreateIndexOptions{ Unique = true }),
            new CreateIndexModel<Employee>(Builders<Employee>.IndexKeys.Ascending(x => x.isActive))
        });

        await Project.Indexes.CreateOneAsync(
            new CreateIndexModel<Project>(Builders<Project>.IndexKeys.Ascending(x => x.code),
            new CreateIndexOptions{ Unique = true }));

        await TimeEntryProject.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<TimeEntryProject>(Builders<TimeEntryProject>.IndexKeys.Ascending(x => x.employeeId)),
            new CreateIndexModel<TimeEntryProject>(Builders<TimeEntryProject>.IndexKeys.Ascending(x => x.projectId)),
            new CreateIndexModel<TimeEntryProject>(Builders<TimeEntryProject>.IndexKeys.Ascending(x => x.date))
        });

        await GiangVien.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<GiangVien>(Builders<GiangVien>.IndexKeys.Ascending(x => x.email)),
            new CreateIndexModel<GiangVien>(Builders<GiangVien>.IndexKeys.Ascending(x => x.isActive))
        });

        await LopHoc.Indexes.CreateOneAsync(
            new CreateIndexModel<LopHoc>(Builders<LopHoc>.IndexKeys.Ascending(x => x.isActive)));
    }
}
