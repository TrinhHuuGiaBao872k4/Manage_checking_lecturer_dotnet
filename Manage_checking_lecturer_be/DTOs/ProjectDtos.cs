namespace MongoElearn.DTOs.Projects;

public record ProjectCreateDto(
    string code,
    string name,
    string? description,
    string status,
    string type,
    int limit,
    DateTime startDate,
    DateTime endDate,
    string? typeLeave,
    bool? isActive
);

public record ProjectUpdateDto(
    string? code,
    string? name,
    string? description,
    string? status,
    string? type,
    int? limit,
    DateTime? startDate,
    DateTime? endDate,
    string? typeLeave,
    bool? isActive
);
