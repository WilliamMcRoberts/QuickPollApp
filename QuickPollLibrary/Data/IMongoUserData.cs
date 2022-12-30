namespace QuickPollLibrary.Data
{
    public interface IMongoUserData : IBaseData<UserModel>
    {
        Task<UserModel> GetCurrentUserByUserId(string userId);
        Task<UserModel> GetCurrentUserFromAuthentication(string objectId);
        Task UpdateUser(UserModel user);
    }
}