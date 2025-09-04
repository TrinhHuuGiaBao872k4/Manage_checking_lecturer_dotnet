namespace MongoElearn.DTOs.Employees;

public record EmployeeCreateDto(
    string code,
    string email,
    string password,
    string fullname,
    string? currentSchool,
    string? internshipPosition,
    DateTime? internshipStartTime,
    DateTime? internshipEndTime,
    IEnumerable<string>? skills,
    IEnumerable<string>? roles,
    bool? isActive ,
    string? managerId
);

public record EmployeeUpdateDto(
    string? code,
    string? email,
    string? password,
    string? fullname,
    string? currentSchool,
    string? internshipPosition,
    DateTime? internshipStartTime,
    DateTime? internshipEndTime,
    IEnumerable<string>? skills,
    IEnumerable<string>? roles,
    string? managerId,
    bool? isActive
);


public record EmployeeViewDto(
    string id, string code, string email, string fullname,
    string? currentSchool, string? internshipPosition,
    DateTime? internshipStartTime, DateTime? internshipEndTime,
    IEnumerable<string>? skills, IEnumerable<string> roles,
    bool isActive, string? managerId
);
