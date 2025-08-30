using MongoDB.Driver;
using MongoElearn.Infrastructure;
using MongoElearn.Models;
using MongoElearn.DTOs.Roles;
using MongoElearn.Infrastructure.Repositories;
namespace MongoElearn.Services;
public interface IRoleService
{
    Task<List<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(int id);
    Task CreateAsync(RoleCreateDto dto);
    Task<bool> UpdateAsync(int id, RoleUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
public class RoleService : IRoleService
{
    private readonly IRoleRepository _repo;
    public RoleService(IRoleRepository repo) => _repo = repo;

    public Task<List<Role>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Role?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(RoleCreateDto dto)
    {
        var doc = new Role { id = dto.id, name = dto.name };
        return _repo.CreateAsync(doc);
    }

    public async Task<bool> UpdateAsync(int id, RoleUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        e.name = dto.name ?? e.name;
        return await _repo.ReplaceAsync(Builders<Role>.Filter.Eq(x => x.id, id), e);
    }

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(Builders<Role>.Filter.Eq(x => x.id, id));
}
