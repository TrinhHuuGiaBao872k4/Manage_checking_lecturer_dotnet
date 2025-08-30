using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoElearn.Models;

public class Counter
{
    // tên sequence, ví dụ: "Employee", "Project", ...
    [BsonId] public string Id { get; set; } = default!;

    [BsonElement("seq")] public long Seq { get; set; }
}
