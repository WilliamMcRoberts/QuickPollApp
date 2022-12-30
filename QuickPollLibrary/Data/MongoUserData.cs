

namespace QuickPollLibrary.Data;

public class MongoUserData : BaseData<UserModel>, IMongoUserData
{

    public MongoUserData(IMongoDbConnection mongoDbConnection) : base(mongoDbConnection, "users")
    {
    }

    public async Task<UserModel> GetCurrentUserByUserId(string userId)
    {
        var users = await _collection.FindAsync(u => u.UserId == userId);

        return users.FirstOrDefault();
    }

    public async Task<UserModel> GetCurrentUserFromAuthentication(string objectId)
    {
        var users = await _collection.FindAsync(u => u.ObjectIdentifier == objectId);

        return users.FirstOrDefault();
    }

    public Task UpdateUser(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq("UserId", user.UserId);

        return _collection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
    }
}
