

namespace QuickPollLibrary.Services;

public class PollService : IPollService
{
    private readonly IHubContext<PollHub> _hubContext;
    private List<PollModel> _activePolls = new();
    private List<PollModel> _finishedPolls = new();
    public List<PollModel> AllPolls { get; set; } = new();

    public PollService(IHubContext<PollHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task AddPoll(PollModel poll)
    {
        AllPolls.Add(poll);
        return _hubContext.Clients.All.SendAsync(method: "getAllPolls", "Server/PollHub", AllPolls);
    }

    public Task RemovePoll(Guid pollId)
    {
        var pollToRemove = AllPolls.Where(p => p.PollId == pollId).FirstOrDefault();
        if (pollToRemove is not null) AllPolls.Remove(pollToRemove);
        return _hubContext.Clients.All.SendAsync(method: "getAllPolls", "Server/PollHub", AllPolls);
    }

    public Task Vote(Guid pollId, PollOptionModel option)
    {
        var pollToVoteOn = AllPolls.Where(p => p.PollId == pollId)
                            .FirstOrDefault();

        if (pollToVoteOn is null) return Task.CompletedTask;

        var optionToVoteOn = pollToVoteOn.OptionsList.Where(o => o.PollOptionName == option.PollOptionName)
                                .FirstOrDefault();

        if (optionToVoteOn is not null) optionToVoteOn.PollOptionVotes++;

        return _hubContext.Clients.All.SendAsync("getPoll", pollToVoteOn.PollId, pollToVoteOn);
    }

    public Task BroadcastAllPolls() =>
        _hubContext.Clients.All.SendAsync(method: "getAllPolls", "Server/PollHub", AllPolls);

    public Task BroadcastActivePolls() =>
        _hubContext.Clients.All.SendAsync("getActivePolls", DateTime.Now, _activePolls);

    public Task BroadcastFinishedPolls() =>
        _hubContext.Clients.All.SendAsync("getFinishedPolls", DateTime.Now, _finishedPolls);

    public Task BroadcastPoll(PollModel poll) =>
        _hubContext.Clients.All.SendAsync("getPoll", $"{poll.PollId}", poll);
}
