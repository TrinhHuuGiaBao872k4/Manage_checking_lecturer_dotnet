namespace MongoElearn.DTOs.TimeEntries;

public record TimeEntryCreateDto(
    DateTime date,
    int hours,        
    string employeeId,
    string projectId,
    bool? isActive
);

public record TimeEntryUpdateDto(
    DateTime? date,
    int? hours,
    string? employeeId,
    string? projectId,
    bool? isActive
);
