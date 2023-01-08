namespace QuickPollLibrary.Data
{
    public interface IMongoUserData : IBaseData<UserModel>
    {
        Task<UserModel> GetCurrentUserFromAuthentication(string objectId);
    }
}