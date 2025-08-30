using MongoDB.Driver;
using MongoElearn.Models;
using MongoElearn.Infrastructure;
using MongoElearn.DTOs.Employees;
using MongoElearn.Infrastructure.Repositories;
using System.Linq;
using MongoDB.Bson;
namespace MongoElearn.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(string id);
    Task<Employee> CreateAsync(EmployeeCreateDto dto);
    Task<bool> UpdateAsync(string id, EmployeeUpdateDto dto);
    Task<bool> DeleteAsync(string id);

    Task<Employee?> GetByEmailAsync(string email);
    Task<List<Employee>> SearchAsync(string? keyword, bool? isActive, string? role);
}
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;
     private readonly ISequenceService _seq;
    public EmployeeService(IEmployeeRepository repo, ISequenceService seq)
    {
        _repo = repo; _seq = seq;
    }
    public EmployeeService(IEmployeeRepository repo) => _repo = repo;


    public Task<List<Employee>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Employee?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
    public Task<Employee?> GetByEmailAsync(string email) => _repo.GetByEmailAsync(email);
    public Task<List<Employee>> SearchAsync(string? keyword, bool? isActive, string? role)
        => _repo.SearchAsync(keyword, isActive, role);

    public async Task<Employee> CreateAsync(EmployeeCreateDto dto)
    {
        
        var emp = new Employee
        {
            code = dto.code,
            email = dto.email,
            password = BCrypt.Net.BCrypt.HashPassword(dto.password),
            fullname = dto.fullname,
            current_school = dto.currentSchool,
            internship_position = dto.internshipPosition,
            internship_start_time = dto.internshipStartTime,
            internship_end_time = dto.internshipEndTime,
            Skills = dto.skills?.ToList(),
            role = dto.roles?.ToList() ?? new List<string> { "Intern" },
            isActive = true,
            manager = dto.managerId
        };
         var next = await _seq.GetNextAsync("Employee");
        emp.id = next.ToString();
        await _repo.CreateAsync(emp);
        return emp;
    }

    public async Task<bool> UpdateAsync(string id, EmployeeUpdateDto dto)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e is null) return false;

        e.code = dto.code ?? e.code;
        e.email = dto.email ?? e.email;
        if (!string.IsNullOrEmpty(dto.password))
            e.password = BCrypt.Net.BCrypt.HashPassword(dto.password);
        e.fullname = dto.fullname ?? e.fullname;
        e.current_school = dto.currentSchool ?? e.current_school;
        e.internship_position = dto.internshipPosition ?? e.internship_position;
        e.internship_start_time = dto.internshipStartTime ?? e.internship_start_time;
        e.internship_end_time = dto.internshipEndTime ?? e.internship_end_time;
        if (dto.skills is not null) e.Skills = dto.skills.ToList();
        if (dto.roles is not null) e.role = dto.roles.ToList();
        if (dto.isActive.HasValue) e.isActive = dto.isActive.Value;
        e.manager = dto.managerId ?? e.manager;

        return await _repo.ReplaceAsync(Builders<Employee>.Filter.Eq(x => x.id, id), e);
    }

    public Task<bool> DeleteAsync(string id)
        => _repo.DeleteAsync(Builders<Employee>.Filter.Eq(x => x.id, id));
}
