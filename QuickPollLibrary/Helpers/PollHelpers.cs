

namespace QuickPollLibrary.Helpers;

public static class PollHelpers
{
    public static decimal GetPercentage(this int votes, int totalVotes) =>
        totalVotes == 0 ? 0 : Math.Round((100 / (decimal)totalVotes * votes), 2);

    public static int GetTotalVotes(this List<PollOptionModel> pollOptions)
    {
        int total = 0;

        foreach (var option in pollOptions)
            total += option.PollOptionVotes;

        return total;
    }
}
