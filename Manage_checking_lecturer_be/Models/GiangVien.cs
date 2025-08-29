// Models/GiangVien.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class GiangVien
{
    [BsonId, BsonRepresentation(BsonType.Int32)]
    public int id { get; set; }

    [BsonElement("Ten")] public string? Ten { get; set; }
    [BsonElement("KiNang")] public string? KiNang { get; set; }
    [BsonElement("ChiNhanh")] public string? ChiNhanh { get; set; }
    [BsonElement("KhungGioDay")] public string? KhungGioDay { get; set; }
    [BsonElement("DayOff")] public DateTime? DayOff { get; set; }
    [BsonElement("Mau")] public string? Mau { get; set; }
    [BsonElement("GhiChu")] public string? GhiChu { get; set; }
    [BsonElement("email")] public string? email { get; set; }
    [BsonElement("isActive")] public bool isActive { get; set; } = true;
    [BsonElement("account")] public string? account { get; set; }
    [BsonElement("password")] public string? password { get; set; }
    [BsonElement("role")] public object? role { get; set; } // JSON
}
