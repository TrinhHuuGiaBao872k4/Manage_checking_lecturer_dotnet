using MongoDB.Driver;
using MongoElearn.Models;

namespace MongoElearn.Infrastructure;

public interface ISequenceService
{
    Task<long> GetNextAsync(string name);
}

public class SequenceService : ISequenceService
{
    private readonly IMongoCollection<Counter> _col;
    public SequenceService(MongoDbContext ctx) => _col = ctx.Counters;

    public async Task<long> GetNextAsync(string name)
    {
        var filter = Builders<Counter>.Filter.Eq(x => x.Id, name);
        var update = Builders<Counter>.Update.Inc(x => x.Seq, 1);
        var opts = new FindOneAndUpdateOptions<Counter>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };
        var result = await _col.FindOneAndUpdateAsync(filter, update, opts);
        return result.Seq; // 1,2,3...
    }
}
