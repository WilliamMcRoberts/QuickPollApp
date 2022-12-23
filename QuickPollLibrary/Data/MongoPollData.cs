
namespace QuickPollLibrary.Data;

public class MongoPollData : IMongoPollData
{
    private readonly IMongoCollection<PollModel> _polls;

    public MongoPollData(IMongoDbConnection mongoDbConnection)
    {
        _polls = mongoDbConnection.PollsCollection;
    }

    public async Task<List<PollModel>> GetAllPolls()
    {
        var result = await _polls.FindAsync(new BsonDocument());

        var polls = result.ToList();

        return polls;
    }

    public async Task<PollModel> GetPollById(Guid pollId)
    {
        var polls = await _polls.FindAsync(p => p.PollId == pollId);

        return polls.FirstOrDefault();
    }

    public async Task<Guid> CreatePoll(PollModel poll)
    {
        await _polls.InsertOneAsync(poll);

        return poll.PollId;
    }

    public Task UpdatePoll(PollModel poll)
    {
        var filter = Builders<PollModel>.Filter.Eq("PollId", poll.PollId);

        return _polls.ReplaceOneAsync(filter, poll, new ReplaceOptions { IsUpsert = true });
    }
}
