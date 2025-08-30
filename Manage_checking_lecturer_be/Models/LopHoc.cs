// Models/LopHoc.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class LopHoc
{
    [BsonId, BsonRepresentation(BsonType.Int32)]
    public int LopHoc_Id { get; set; }

    [BsonElement("TenLopHoc")] public string? TenLopHoc { get; set; }
    [BsonElement("KhoaHoc")] public string? KhoaHoc { get; set; }
    [BsonElement("ThoiKhoaBieu")] public string? ThoiKhoaBieu { get; set; }
    [BsonElement("ChiNhanhLop")] public string? ChiNhanhLop { get; set; }
    [BsonElement("NgayBatDau")] public DateTime? NgayBatDau { get; set; }
    [BsonElement("NgayKetThuc")] public DateTime? NgayKetThuc { get; set; }
    [BsonElement("khungGio")] public string? khungGio { get; set; } // giữ string như MySQL time
    [BsonElement("isActive")] public bool isActive { get; set; } = true;
}
