

namespace QuickPollLibrary.Hubs;

public class PollHub : Hub
{
    public int ConnectionCount { get; set; }

    private List<PollModel> _allPolls = new();



    public void AddPoll(PollModel poll)
    {
        _allPolls.Add(poll);
        BroadcastAllPolls();
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

        BroadcastAllPolls();
    }

    public void UpdatePoll(PollModel poll)
    {
        var pollToUpdate = _allPolls.FirstOrDefault(p => p.PollId == poll.PollId);

        if (pollToUpdate is null) return;

        _allPolls[_allPolls.IndexOf(pollToUpdate)] = poll;

        BroadcastPoll(poll);
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

    public PollModel GetPollById(string pollId)
    {
        return _allPolls.FirstOrDefault(p => p.PollId.ToString() == pollId)!;
    }

    public Task BroadcastAllPolls()
    {
        if (_allPolls.Count == 0) return Task.CompletedTask;
        return Clients.All.SendAsync(method: "getAllPolls", "PollService", _allPolls);
    }

    public Task BroadcastPoll(PollModel pollToBroadcast)
    {
        return Clients.All.SendAsync(method: "getPoll", pollToBroadcast);
    }

    public Task SendCount()
    {
        return Clients.All.SendAsync("ReceiveCount", ConnectionCount);
    }

    public override Task OnConnectedAsync()
    {
        ConnectionCount++;

        SendCount();

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        ConnectionCount--;

        SendCount();

        return base.OnDisconnectedAsync(exception);
    }

}
