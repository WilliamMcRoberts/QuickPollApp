
using Microsoft.Extensions.Caching.Memory;

namespace QuickPollLibrary.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    protected readonly IMongoDbConnection _mongoDbConnection;
    public UnitOfWork(IMongoDbConnection mongoDbConnection, IMemoryCache memoryCache)
    {
        _mongoDbConnection = mongoDbConnection;
        Polls = new MongoPollData(_mongoDbConnection);
        Users = new MongoUserData(_mongoDbConnection, memoryCache);
    }

    public IMongoPollData Polls { get; private set; }
    public IMongoUserData Users { get; private set; }
}
