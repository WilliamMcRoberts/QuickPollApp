namespace QuickPollLibrary.Managers
{
    public interface IPollManager
    {
        Task AddPoll(PollModel poll);
        Task BroadcastAllPolls();
        Task BroadcastPoll(PollModel pollToBroadcast);
        Task DeletePoll(PollModel poll, UserModel loggedInUser);
        Task<PollModel> GetPollById(string pollId);
        Task UndoVote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
        Task UpdatePoll(PollModel poll);
        Task Vote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
        Task VoteOrUndoVoteForOption(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
    }
}