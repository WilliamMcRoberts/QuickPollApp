using Microsoft.AspNetCore.SignalR;
using Moq;
using QuickPollLibrary.Hubs;
using QuickPollLibrary.Managers;
using QuickPollLibrary.Models;

namespace QuickPollTests;

public class TestPollManager
{
    [Fact]
    public void Test_ShouldAddPoll_AddPoll()
    {
        var test_poll = new PollModel();
        var guidOfPoll = test_poll.PollId;
        var fakeHubContext = new Mock<IHubContext<PollHub>>();
        var sut = new PollManager(fakeHubContext.Object);
        var startingPollCount = sut._allPolls.Count;

        sut.AddPoll(test_poll);

        var endingPollCount = sut._allPolls.Count;

        Assert.Equal(startingPollCount + 1, sut._allPolls.Count);
        Assert.Equal(1, endingPollCount);
        Assert.Contains(test_poll, sut._allPolls);
        Assert.Equal(test_poll, sut._allPolls.Where(p => p.PollId == guidOfPoll).First());
    }
}
