
namespace QuickPollLibrary.Services;

public interface IPollService
{
    List<PollModel> AllPolls { get; set; }

    Task AddPoll(PollModel poll);
    Task BroadcastActivePolls();
    Task BroadcastAllPolls();
    Task BroadcastFinishedPolls();
    Task BroadcastPoll(PollModel poll);
    Task RemovePoll(Guid pollId);
    Task Vote(Guid pollId, PollOptionModel option);
}