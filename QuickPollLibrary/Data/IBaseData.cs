
namespace QuickPollLibrary.Data;

public interface IBaseData<T>
{
    Task CreateOneAsync(T item);
    Task DeleteOneAsync(Expression<Func<T, bool>> expression);
    Task<List<T>> GetAllAsync();
    Task<T> GetOneAsync(Expression<Func<T, bool>> expression);
    Task UpdateOneAsync(Expression<Func<T, bool>> expression, UpdateDefinition<T> update);
    Task ReplaceOneAsync(string itemId, T item, string field = "Id");
}