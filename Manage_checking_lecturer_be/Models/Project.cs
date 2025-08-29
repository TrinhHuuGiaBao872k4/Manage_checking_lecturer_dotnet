// Models/Project.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class Project
{
    [BsonId, BsonRepresentation(BsonType.String)]
    public string id { get; set; } = default!;

    [BsonElement("code")] public string code { get; set; } = default!;
    [BsonElement("name")] public string name { get; set; } = default!;
    [BsonElement("description")] public string? description { get; set; }
    [BsonElement("status")] public string status { get; set; } = default!;
    [BsonElement("type")] public string type { get; set; } = default!;
    [BsonElement("limit")] public int limit { get; set; }
    [BsonElement("startedAt")] public DateTime startedAt { get; set; }
    [BsonElement("updatedAt")] public DateTime updatedAt { get; set; }
    [BsonElement("endDate")] public DateTime endDate { get; set; }
    [BsonElement("startDate")] public DateTime startDate { get; set; }
    [BsonElement("typeLeave")] public string? typeLeave { get; set; }
    [BsonElement("isActive")] public bool isActive { get; set; } = true;
}
