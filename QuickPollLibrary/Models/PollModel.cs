
using System.ComponentModel.DataAnnotations;

namespace QuickPollLibrary.Models;

public class PollModel
{
    public Guid PollId { get; init; } = Guid.NewGuid();
    public string PollCreatorId { get; set; } = string.Empty;
    [Required]
    [MaxLength(50, ErrorMessage = "Question should be no longer than 50 characters.")]
    public string Question { get; set; } = string.Empty;
    [ValidateComplexType]
    public List<PollOptionModel> OptionsList { get; set; } = new();
    public List<string> UsersVoted { get; set; } = new();
    public DateTime StartTime { get; set; }
    public TimeSpan? TimeLimit { get; set; }
    public bool HasStarted { get; set; }
    public bool IsComplete { get; set; }
}
