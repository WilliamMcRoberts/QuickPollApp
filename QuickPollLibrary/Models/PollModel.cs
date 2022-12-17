
namespace QuickPollLibrary.Models;

public class PollModel
{
    public Guid PollId { get; init; } = Guid.NewGuid();
    public string PollCreatorId { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public List<PollOptionModel> OptionsList { get; set; } = new();
    public List<string> UsersVoted { get; set; } = new();
    public DateTime StartTime { get; set; }
    public TimeSpan? TimeLimit { get; set; }
    public bool HasStarted { get; set; }
    public bool IsComplete { get; set; }
}
