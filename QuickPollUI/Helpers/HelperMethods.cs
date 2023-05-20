using QuickPollLibrary.Models;

namespace QuickPollUI.Helpers;

public static class HelperMethods
{
    public static decimal GetPercentage(this int votes, int totalVotes)
    {
        return totalVotes == 0 ? 0 : Math.Round(100 / (decimal)totalVotes * votes, 2);
    }

    public static int GetTotalVotes(this List<PollOptionModel> pollOptions)
    {
        int total = 0;

        foreach (var option in pollOptions)
            total += option.PollOptionVotes;

        return total;
    }

    public static string GetTooltipText(this PollOptionModel option, PollModel poll, UserModel loggedInUser)
    {
        if (poll.IsComplete) return "Poll Is Finished";
        return loggedInUser is null ? "You Must Be Logged In To Vote"
                : poll.UsersVoted.Contains(loggedInUser?.UserId!) && option.PollOptionUsersVoted.Contains(loggedInUser?.UserId!) ? $"Undo Vote {option.PollOptionName}"
                : poll.UsersVoted.Contains(loggedInUser?.UserId!) ? "You've Already Voted"
                : !poll.HasStarted ? "Poll Has Not Started Yet"
                : $"Vote {option.PollOptionName}";
    }
}
