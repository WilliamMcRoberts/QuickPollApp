
using QuickPollLibrary.Services;

namespace QuickPollLibrary.Helpers;

public static class PollHelpers
{
    public static async Task Vote(this PollOptionModel pollOption, PollModel poll, 
            UserModel loggedInUser, IPollService pollService)
    {
        poll.UsersVoted.Add(loggedInUser.UserId);

        pollOption.PollOptionUsersVoted.Add(loggedInUser.UserId);

        poll.OptionsList[poll.OptionsList.IndexOf(pollOption)].PollOptionVotes++;

        await pollService.UpdatePoll(poll);
    }

    public static async Task UndoVote(this PollOptionModel pollOption, PollModel poll, 
            UserModel loggedInUser, IPollService pollService)
    {
        poll.UsersVoted.Remove(loggedInUser.UserId);

        pollOption.PollOptionUsersVoted.Remove(loggedInUser.UserId);

        poll.OptionsList[poll.OptionsList.IndexOf(pollOption)].PollOptionVotes--;

        await pollService.UpdatePoll(poll);
    }

    public static void DeletePoll(
        this PollModel poll, UserModel loggedInUser, IPollService pollService)
    {
        if (poll.PollCreatorId != loggedInUser.UserId
            || DateTime.Now < poll.StartTime + poll.TimeLimit) return;

        pollService.RemovePoll(poll);
    }

    public static decimal GetPercentage(this int votes, int totalVotes) =>
        totalVotes == 0 ? 0 : Math.Round((100 / (decimal)totalVotes * votes), 2);

    public static int GetTotalVotes(this List<PollOptionModel> pollOptions)
    {
        int total = 0;

        foreach (var option in pollOptions)
            total += option.PollOptionVotes;

        return total;
    }

    public static async Task VoteOrUndoVoteForOption(
        this PollOptionModel pollOption, PollModel poll, UserModel loggedInUser, IPollService pollService)
    {
        if (poll.IsComplete || !poll.HasStarted)
            return;

        if (poll.UsersVoted.Contains(loggedInUser.UserId) &&
            pollOption.PollOptionUsersVoted.Contains(loggedInUser.UserId))
        {
            await pollOption.UndoVote(poll, loggedInUser, pollService);
            return;
        }

        if (poll.UsersVoted.Contains(loggedInUser.UserId)) return;

        await pollOption.Vote(poll, loggedInUser, pollService);
    }
}
