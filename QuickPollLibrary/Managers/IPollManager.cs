namespace QuickPollLibrary.Managers
{
    public interface IPollManager
    {
        void AddPoll(PollModel poll);
        Task BroadcastAllPolls();
        Task BroadcastPoll(PollModel pollToBroadcast);
        void DeletePoll(PollModel poll, UserModel loggedInUser);
        Task<PollModel> GetPollById(string pollId);
        void UndoVote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
        void UpdatePoll(PollModel poll);
        void Vote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
        void VoteOrUndoVoteForOption(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
    }
}