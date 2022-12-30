using System.Linq.Expressions;

namespace QuickPollLibrary.Data
{
    public interface IBaseData<T>
    {
        Task CreateOne(T item);
        Task DeletePoll(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAll();
        Task<T> GetOne(Expression<Func<T, bool>> expression);
        Task UpdateOne(Expression<Func<T, bool>> expression, UpdateDefinition<T> update);
    }
}