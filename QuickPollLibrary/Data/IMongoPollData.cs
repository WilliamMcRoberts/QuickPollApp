namespace QuickPollLibrary.Data;

public interface IMongoPollData : IBaseData<PollModel>
{
    Task<List<PollModel>> GetFirst100Polls();
}