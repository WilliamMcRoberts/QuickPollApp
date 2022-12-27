

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

    public async Task AddPoll(PollModel poll)
    {
        AllPolls.Add(poll);
        await BroadcastAllPolls();
    }

    public Task RemovePoll(PollModel poll)
    {
        var pollToRemove = AllPolls.FirstOrDefault(p => p.PollId == poll.PollId);

        if (pollToRemove is null) return Task.CompletedTask;

        AllPolls.Remove(pollToRemove);

        return BroadcastAllPolls();
    }

    public Task UpdatePoll(PollModel poll)
    {
        var pollToUpdate = AllPolls.FirstOrDefault(p => p.PollId == poll.PollId);

        if (pollToUpdate is null) return Task.CompletedTask;

        AllPolls[AllPolls.IndexOf(pollToUpdate)] = poll;

        return BroadcastAllPolls();
    }

    public Task BroadcastAllPolls() =>
        _hubContext.Clients.All.SendAsync(method: "getAllPolls", "PollService", AllPolls);

    public Task BroadcastActivePolls() =>
        _hubContext.Clients.All.SendAsync("getActivePolls", DateTime.Now, _activePolls);

    public Task BroadcastFinishedPolls() =>
        _hubContext.Clients.All.SendAsync("getFinishedPolls", DateTime.Now, _finishedPolls);

    public Task BroadcastPoll(PollModel poll) =>
        _hubContext.Clients.All.SendAsync("getPoll", $"{poll.PollId}", poll);
}
