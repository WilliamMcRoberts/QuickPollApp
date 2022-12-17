
namespace QuickPollLibrary.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string from, string message)
    {
        return Clients.All.SendAsync("getMessage", from, message);
    }

}
