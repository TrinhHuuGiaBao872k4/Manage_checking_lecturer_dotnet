// Models/Employee.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class Employee
{
    [BsonId, BsonRepresentation(BsonType.String)]
    public string id { get; set; } = default!;

    [BsonElement("code")] public string code { get; set; } = default!;
    [BsonElement("email")] public string email { get; set; } = default!;
    [BsonElement("password")] public string password { get; set; } = default!;
    [BsonElement("fullname")] public string fullname { get; set; } = default!;
    [BsonElement("current_school")] public string? current_school { get; set; }
    [BsonElement("internship_end_time")] public DateTime? internship_end_time { get; set; }
    [BsonElement("internship_position")] public string? internship_position { get; set; }
    [BsonElement("internship_start_time")] public DateTime? internship_start_time { get; set; }
    [BsonElement("skills")] public string? skills { get; set; } 
    [BsonElement("isActive")] public bool isActive { get; set; } = true;
    [BsonElement("manager")] public string? manager { get; set; }
    [BsonElement("role")] public List<string>? role { get; set; } 
}
