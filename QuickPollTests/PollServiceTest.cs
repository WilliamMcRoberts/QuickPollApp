
using FakeItEasy;
using QuickPollLibrary.Models;
using QuickPollLibrary.Services;

namespace QuickPollTests;

public class PollServiceTest
{
    private PollService _test_PollService;

    public PollServiceTest()
    {
        _test_PollService = A.Fake<PollService>();
    }

    [Fact]
    public async void AddPoll_ShouldAddPollToAllPollsList()
    {
        PollModel poll = new();

        await _test_PollService.AddPoll(poll);

        Assert.Contains(poll, _test_PollService.AllPolls);
    }

    [Fact]
    public async void RemovePoll_ShouldRemovePollFromAllPollsList()
    {
        PollModel poll = new();

        await _test_PollService.AddPoll(poll);

        Assert.Contains(poll, _test_PollService.AllPolls);

        await _test_PollService.RemovePoll(poll);

        Assert.DoesNotContain(poll, _test_PollService.AllPolls);
    }
}