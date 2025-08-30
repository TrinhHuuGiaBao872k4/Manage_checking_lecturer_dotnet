namespace MongoElearn.DTOs.GiangVien;

public record GiangVienCreateDto(
    string Ten,
    string? KiNang,
    string? ChiNhanh,
    string? KhungGioDay,
    DateTime? DayOff,
    string? Mau,
    string? GhiChu,
    string? email,
    string? account,
    string? password,   
    object? role,
    bool? isActive
);

public record GiangVienUpdateDto(
    string? Ten,
    string? KiNang,
    string? ChiNhanh,
    string? KhungGioDay,
    DateTime? DayOff,
    string? Mau,
    string? GhiChu,
    string? email,
    string? account,
    string? password,   // sẽ hash nếu có
    object? role,
    bool? isActive
);
