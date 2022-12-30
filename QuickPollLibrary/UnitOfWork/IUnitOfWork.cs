namespace QuickPollLibrary.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMongoPollData Polls { get; }
        IMongoUserData Users { get; }
    }
}