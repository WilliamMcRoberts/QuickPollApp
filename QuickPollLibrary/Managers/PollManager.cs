
namespace QuickPollLibrary.Managers;

public class PollManager : IPollManager
{
    private readonly IHubContext<PollHub> _hubContext;
    public List<PollModel> _allPolls = new();

    public PollManager(IHubContext<PollHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public void AddPoll(PollModel poll)
    {
        _allPolls.Add(poll);
    }

    public void DeletePoll(PollModel poll, UserModel loggedInUser)
    {
        if (poll.PollCreatorId != loggedInUser.UserId || DateTime.Now < poll.StartTime + poll.TimeLimit)
            return;

        var pollToRemove = _allPolls.FirstOrDefault(p => p.PollId == poll.PollId);

        if (pollToRemove is null) return;

        if (loggedInUser.PollIds.Contains(poll.PollId.ToString()))
            loggedInUser.PollIds.Remove(pollToRemove.PollId.ToString());

        _allPolls.Remove(pollToRemove);
    }

    public void UpdatePoll(PollModel poll)
    {
        var pollToUpdate = _allPolls.FirstOrDefault(p => p.PollId == poll.PollId);

        if (pollToUpdate is null) return;

        _allPolls[_allPolls.IndexOf(pollToUpdate)] = poll;
    }


    public void Vote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser)
    {
        poll.UsersVoted.Add(loggedInUser.UserId);

        pollOption.PollOptionUsersVoted.Add(loggedInUser.UserId);

        poll.OptionsList[poll.OptionsList.IndexOf(pollOption)].PollOptionVotes++;

        UpdatePoll(poll);
    }

    public void UndoVote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser)
    {
        poll.UsersVoted.Remove(loggedInUser.UserId);

        pollOption.PollOptionUsersVoted.Remove(loggedInUser.UserId);

        poll.OptionsList[poll.OptionsList.IndexOf(pollOption)].PollOptionVotes--;

        UpdatePoll(poll);

    }

    public void VoteOrUndoVoteForOption(
        PollOptionModel pollOption, PollModel poll, UserModel loggedInUser)
    {
        if (poll.IsComplete || !poll.HasStarted || loggedInUser is null)
            return;

        if (poll.UsersVoted.Contains(loggedInUser.UserId) &&
            pollOption.PollOptionUsersVoted.Contains(loggedInUser.UserId))
        {
            UndoVote(pollOption, poll, loggedInUser);
            return;
        }

        if (poll.UsersVoted.Contains(loggedInUser.UserId)) return;

        Vote(pollOption, poll, loggedInUser);
    }

    public async Task<PollModel> GetPollById(string pollId)
    {
        return await Task.FromResult(_allPolls.FirstOrDefault(p => p.PollId.ToString() == pollId)!);
    }

    public async Task BroadcastAllPolls()
    {
        await _hubContext.Clients.All.SendAsync(method: "getAllPolls", "PollService", _allPolls);
    }

    public async Task BroadcastPoll(PollModel pollToBroadcast)
    {
        await _hubContext.Clients.All.SendAsync(method: "getPoll", pollToBroadcast);
    }
}
