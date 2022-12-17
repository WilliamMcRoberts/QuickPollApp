
namespace QuickPollLibrary.Data;

public interface IMongoPollData
{
    Task<Guid> CreatePoll(PollModel poll);
    Task<List<PollModel>> GetAllPolls();
    Task<PollModel> GetPollById(Guid pollId);
    Task UpdatePoll(PollModel poll);
}