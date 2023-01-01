
namespace QuickPollLibrary.Models;

public class PollOptionModel
{
    public Guid PollOptionId { get; init; } = Guid.NewGuid();
    public string PollOptionName { get; set; } = string.Empty;
    public List<string> PollOptionUsersVoted { get; set; } = new();
    public int PollOptionVotes { get; set; }
}
