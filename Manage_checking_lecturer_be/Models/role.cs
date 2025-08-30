// Models/role.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class role
{
    [BsonId, BsonRepresentation(BsonType.Int32)]
    public int id { get; set; }

    [BsonElement("name")] public string? name { get; set; }
}
