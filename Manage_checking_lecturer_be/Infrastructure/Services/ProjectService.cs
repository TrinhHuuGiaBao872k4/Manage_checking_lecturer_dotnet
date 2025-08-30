using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;
using MongoElearn.DTOs.Projects;
using MongoElearn.Infrastructure.Repositories;
namespace MongoElearn.Services;

public interface IProjectService
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(string id);
    Task<Project> CreateAsync(ProjectCreateDto dto);
    Task<bool> UpdateAsync(string id, ProjectUpdateDto dto);
    Task<bool> DeleteAsync(string id);

    Task<Project?> GetByCodeAsync(string code);
}

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repo;
     private readonly ISequenceService _seq;
    public ProjectService(IProjectRepository repo, ISequenceService seq)
    { _repo = repo; _seq = seq; }
    public ProjectService(IProjectRepository repo) => _repo = repo;

    public Task<List<Project>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Project?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
    public Task<Project?> GetByCodeAsync(string code) => _repo.GetByCodeAsync(code);

    public async Task<Project> CreateAsync(ProjectCreateDto dto)
    {
        var now = DateTime.UtcNow;
        var doc = new Project
        {
            code = dto.code,
            name = dto.name,
            description = dto.description,
            status = dto.status,
            type = dto.type,
            limit = dto.limit,
            startDate = dto.startDate,
            endDate = dto.endDate,
            typeLeave = dto.typeLeave,
            isActive = dto.isActive ?? true,
            startedAt = now,
            updatedAt = now
        };
         var next = await _seq.GetNextAsync("Project");
        doc.id = next.ToString();
        await _repo.CreateAsync(doc);
        return doc;
    }

    public async Task<bool> UpdateAsync(string id, ProjectUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        e.code = dto.code ?? e.code;
        e.name = dto.name ?? e.name;
        e.description = dto.description ?? e.description;
        e.status = dto.status ?? e.status;
        e.type = dto.type ?? e.type;
        if (dto.limit.HasValue) e.limit = dto.limit.Value;
        e.startDate = dto.startDate ?? e.startDate;
        e.endDate = dto.endDate ?? e.endDate;
        e.typeLeave = dto.typeLeave ?? e.typeLeave;
        if (dto.isActive.HasValue) e.isActive = dto.isActive.Value;
        e.updatedAt = DateTime.UtcNow;

        return await _repo.ReplaceAsync(Builders<Project>.Filter.Eq(x => x.id, id), e);
    }

    public Task<bool> DeleteAsync(string id)
        => _repo.DeleteAsync(Builders<Project>.Filter.Eq(x => x.id, id));
}
