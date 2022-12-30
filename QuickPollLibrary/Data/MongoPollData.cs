
namespace QuickPollLibrary.Data;

public class MongoPollData : BaseData<PollModel>, IMongoPollData
{
    public MongoPollData(
        IMongoDbConnection mongoDbConnection) : base(mongoDbConnection, "polls")
    {
    }

    public async Task<List<PollModel>> GetFirst100Polls()
    {
        var result = await _collection.FindAsync(new BsonDocument());
        return result.ToList().Take(100).ToList();
    }
}
