
namespace QuickPollLibrary.Services;

public class ChatService : IChatService
{
    private IHubContext<ChatHub> _hubContext;

    public ChatService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage()
    {
        await _hubContext.Clients.All.SendAsync("getMessage", "Chat Service", "My message");
    }
}
