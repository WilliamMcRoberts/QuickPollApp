
namespace QuickPollLibrary.DbAccess;

public interface IMongoDbConnection
{
    MongoClient Client { get; }
    string DatabaseName { get; }
    IMongoCollection<PollModel> PollsCollection { get; }
    string PollsCollectionName { get; }
    IMongoCollection<UserModel> UsersCollection { get; }
    string UsersCollectionName { get; }
}