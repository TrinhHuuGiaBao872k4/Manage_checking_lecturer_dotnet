namespace MongoElearn.DTOs.Roles;

public record RoleCreateDto(
    int id,        // bạn đang dùng int id; nếu về sau dùng ObjectId thì bỏ id khỏi Create
    string name
);

public record RoleUpdateDto(
    string? name
);
