

using Microsoft.Extensions.Caching.Memory;

namespace QuickPollLibrary.Data;

public class MongoUserData : BaseData<UserModel>, IMongoUserData
{
    private readonly IMongoDbConnection _mongoDbConnection;
    private readonly IMemoryCache _memoryCache;

    public MongoUserData(IMongoDbConnection mongoDbConnection, IMemoryCache memoryCache) : base(mongoDbConnection, "users")
    {
        _mongoDbConnection = mongoDbConnection;
        _memoryCache = memoryCache;
    }

    public async Task<UserModel> GetCurrentUserFromAuthentication(string objectId)
    {
        var user = new UserModel();

        _memoryCache.TryGetValue("CurrentLoggedInUser", out user);

        if (user is not null) return user;

        var users = await _mongoDbConnection.UsersCollection.FindAsync(u => u.ObjectIdentifier == objectId);

        user = users.FirstOrDefault();

        _memoryCache.Set("CurrentLoggedInUser", user, TimeSpan.FromMinutes(15));

        return user;
    }
}
