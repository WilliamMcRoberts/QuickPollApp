using FakeItEasy;
using QuickPollLibrary.Managers;
using QuickPollLibrary.Models;


namespace QuickPollTests;

public class PollManagerTests
{
    private PollManager _testPollManager;

	public PollManagerTests()
	{
        _testPollManager = A.Fake<PollManager>();

    }

    [Fact]
    public async void AddPoll_ShouldAddPollToAllPollsList()
    {
        PollModel poll = new();

        await _testPollManager.AddPoll(poll);

        //Assert.Contains(poll, _testPollManager.);
    }

    [Fact]
    public async void RemovePoll_ShouldRemovePollFromAllPollsList()
    {
        PollModel poll = new();
        UserModel user = new();

        await _testPollManager.AddPoll(poll);

        //Assert.Contains(poll, _testPollManager.AllPolls);

        await _testPollManager.DeletePoll(poll, user);

        //Assert.DoesNotContain(poll, _testPollManager.AllPolls);
    }
}
