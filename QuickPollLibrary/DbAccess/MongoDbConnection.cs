
namespace QuickPollLibrary.DbAccess;

public class MongoDbConnection : IMongoDbConnection
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly string _connectionId = "MongoDatabase";

    public string DatabaseName { get; private set; }
    public string UsersCollectionName { get; private set; } = "users";
    public string PollsCollectionName { get; private set; } = "polls";

    public MongoClient Client { get; private set; }
    public IMongoCollection<UserModel> UsersCollection { get; private set; }
    public IMongoCollection<PollModel> PollsCollection { get; private set; }


    public MongoDbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        Client = new MongoClient(_configuration.GetConnectionString(_connectionId));
        DatabaseName = _configuration["DatabaseName"]!;
        _mongoDatabase = Client.GetDatabase(DatabaseName);

        UsersCollection = _mongoDatabase.GetCollection<UserModel>(UsersCollectionName);
        PollsCollection = _mongoDatabase.GetCollection<PollModel>(PollsCollectionName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _mongoDatabase.GetCollection<T>(collectionName);
    }
}
