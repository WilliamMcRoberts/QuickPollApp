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
}
