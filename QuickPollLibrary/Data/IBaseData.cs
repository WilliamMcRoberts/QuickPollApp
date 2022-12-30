
namespace QuickPollLibrary.Data;

public interface IBaseData<T>
{
    Task CreateOneAsync(T item);
    Task DeletePollAsync(Expression<Func<T, bool>> expression);
    Task<List<T>> GetAllAsync();
    Task<T> GetOneAsync(Expression<Func<T, bool>> expression);
    Task UpdateOneAsync(Expression<Func<T, bool>> expression, UpdateDefinition<T> update);
    Task ReplaceOneAsync(string field, string id, T obj);
}