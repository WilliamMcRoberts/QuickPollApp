namespace QuickPollLibrary.Hubs
{
    public interface IPollHub
    {
        void AddPoll(PollModel poll);
        Task BroadcastAllPolls();
        Task BroadcastPoll(PollModel pollToBroadcast);
        void DeletePoll(PollModel poll, UserModel loggedInUser);
        PollModel GetPollById(string pollId);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception exception);
        Task SendCount();
        void UndoVote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
        void UpdatePoll(PollModel poll);
        void Vote(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
        void VoteOrUndoVoteForOption(PollOptionModel pollOption, PollModel poll, UserModel loggedInUser);
    }
}