
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class GiangVien_LopHoc
{
    [BsonId, BsonRepresentation(BsonType.Int32)]
    public int GiangVien_LopHoc_Id { get; set; }

    [BsonElement("GiangVien_Id")] public int? GiangVien_Id { get; set; }
    [BsonElement("LopHoc_Id")] public int? LopHoc_Id { get; set; }
    [BsonElement("isActive")] public bool? isActive { get; set; }
}
