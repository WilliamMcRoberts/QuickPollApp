using Microsoft.AspNetCore.SignalR;
using Moq;
using QuickPollLibrary.Hubs;
using QuickPollLibrary.Managers;
using QuickPollLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPollTests;

public class TestPollManager
{
    [Fact]
    public void Test_ShouldAddPoll_DeletePoll()
    {
        var test_poll = new PollModel();

        var fakeHubContext = new Mock<IHubContext<PollHub>>();

        var sut = new PollManager(fakeHubContext.Object);

        var startingPollCount = sut._allPolls.Count;

        sut.AddPoll(test_poll);

        Assert.Equal(startingPollCount + 1, sut._allPolls.Count);
        Assert.Contains(test_poll, sut._allPolls);
    }
}
