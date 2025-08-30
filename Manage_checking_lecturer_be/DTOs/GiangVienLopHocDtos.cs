namespace MongoElearn.DTOs.Links;

public record GiangVienLopHocCreateDto(
    int GiangVien_Id,
    int LopHoc_Id,
    bool? isActive
);

public record GiangVienLopHocUpdateDto(
    bool? isActive
);
