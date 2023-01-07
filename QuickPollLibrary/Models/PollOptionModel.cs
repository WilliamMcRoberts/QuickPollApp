
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickPollLibrary.Models;

public class PollOptionModel
{
    public Guid PollOptionId { get; init; } = Guid.NewGuid();

    [Required(ErrorMessage = "Poll option is required")]
    [MaxLength(50, ErrorMessage = "Option should be no longer than 50 characters")]
    public string PollOptionName { get; set; } = string.Empty;

    public List<string> PollOptionUsersVoted { get; set; } = new();
    public int PollOptionVotes { get; set; }
}
