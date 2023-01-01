

namespace QuickPollLibrary.Hubs;

public class PollHub : Hub
{
    private readonly ICountManager _countManager;

    public PollHub(ICountManager countManager)
    {
        _countManager = countManager;
    }

    public Task SendCount()
    {
        return Clients.All.SendAsync("ReceiveCount", _countManager.ConnectionCount);
    }

    public override Task OnConnectedAsync()
    {
        _countManager.ConnectionCount++;

        SendCount();

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _countManager.ConnectionCount--;

        SendCount(); 

        return base.OnDisconnectedAsync(exception);
    }

}
