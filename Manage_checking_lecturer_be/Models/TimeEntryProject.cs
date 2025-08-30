// Models/TimeEntryProject.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class TimeEntryProject
{
    [BsonId, BsonRepresentation(BsonType.String)]
    public string id { get; set; } = default!;

    [BsonElement("date")] public DateTime date { get; set; }
    [BsonElement("hours")] public int hours { get; set; }
    [BsonElement("employeeId")] public string employeeId { get; set; } = default!;
    [BsonElement("projectId")] public string projectId { get; set; } = default!;
    [BsonElement("isActive")] public bool isActive { get; set; } = true;
}
