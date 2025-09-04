using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;
using MongoElearn.DTOs.TimeEntries;
using MongoElearn.Infrastructure.Repositories;
namespace MongoElearn.Services;
public interface ITimeEntryProjectService
{
    Task<List<TimeEntryProject>> GetAllAsync();
    Task<TimeEntryProject?> GetByIdAsync(string id);
    Task CreateAsync(TimeEntryCreateDto dto);
    Task<bool> UpdateAsync(string id, TimeEntryUpdateDto dto);
    Task<bool> DeleteAsync(string id);

    Task<List<TimeEntryProject>> GetByEmployeeAsync(string employeeId);
    Task<List<TimeEntryProject>> GetByProjectAsync(string projectId);
}
public class TimeEntryProjectService : ITimeEntryProjectService
{
    private readonly ITimeEntryProjectRepository _repo;
     private readonly ISequenceService _seq;
    public TimeEntryProjectService(ITimeEntryProjectRepository repo, ISequenceService seq)
    { _repo = repo; _seq = seq; }
    

    public Task<List<TimeEntryProject>> GetAllAsync() => _repo.GetAllAsync();
    public Task<TimeEntryProject?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(TimeEntryCreateDto dto)
    {
        var doc = new TimeEntryProject
        {
            date = dto.date.Date.ToUniversalTime(),
            hours = dto.hours,
            employeeId = dto.employeeId,
            projectId = dto.projectId,
            isActive = dto.isActive ?? true
        };
        var next = await _seq.GetNextAsync("TimeEntryProject");
        doc.id = next.ToString();
        await _repo.CreateAsync(doc);
        
    }

    public async Task<bool> UpdateAsync(string id, TimeEntryUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        if (dto.date.HasValue) e.date = dto.date.Value.Date.ToUniversalTime();
        if (dto.hours.HasValue) e.hours = dto.hours.Value;
        if (!string.IsNullOrEmpty(dto.employeeId)) e.employeeId = dto.employeeId!;
        if (!string.IsNullOrEmpty(dto.projectId))  e.projectId = dto.projectId!;
        if (dto.isActive.HasValue) e.isActive = dto.isActive.Value;

        return await _repo.ReplaceAsync(Builders<TimeEntryProject>.Filter.Eq(x => x.id, id), e);
    }

    public Task<bool> DeleteAsync(string id)
        => _repo.DeleteAsync(Builders<TimeEntryProject>.Filter.Eq(x => x.id, id));

    public Task<List<TimeEntryProject>> GetByEmployeeAsync(string employeeId) => _repo.GetByEmployeeAsync(employeeId);
    public Task<List<TimeEntryProject>> GetByProjectAsync(string projectId)   => _repo.GetByProjectAsync(projectId);
}
