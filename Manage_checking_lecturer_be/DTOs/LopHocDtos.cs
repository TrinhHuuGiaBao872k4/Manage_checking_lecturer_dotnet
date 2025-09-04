namespace MongoElearn.DTOs.LopHoc;

public record LopHocCreateDto(
    string? TenLopHoc,
    string? KhoaHoc,
    string? ThoiKhoaBieu,
    string? ChiNhanhLop,
    DateTime? NgayBatDau,
    DateTime? NgayKetThuc,
    string? khungGio,
    bool? isActive
);

public record LopHocUpdateDto(
    string? TenLopHoc,
    string? KhoaHoc,
    string? ThoiKhoaBieu,
    string? ChiNhanhLop,
    DateTime? NgayBatDau,
    DateTime? NgayKetThuc,
    string? khungGio,
    bool? isActive
);
