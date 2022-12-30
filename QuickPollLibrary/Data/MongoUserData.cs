

namespace QuickPollLibrary.Data;

public class MongoUserData : BaseData<UserModel>, IMongoUserData
{

    public MongoUserData(IMongoDbConnection mongoDbConnection) : base(mongoDbConnection, "users")
    {
    }

}
