
namespace QuickPollLibrary.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    protected readonly IMongoDbConnection _mongoDbConnection;
    public UnitOfWork(IMongoDbConnection mongoDbConnection)
    {
        _mongoDbConnection = mongoDbConnection;
        Polls = new MongoPollData(_mongoDbConnection);
        Users = new MongoUserData(_mongoDbConnection);
    }

    public IMongoPollData Polls { get; private set; }
    public IMongoUserData Users { get; private set; }
}
