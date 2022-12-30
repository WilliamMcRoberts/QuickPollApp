
using DnsClient.Internal;
using Microsoft.Extensions.Logging;

namespace QuickPollLibrary.Data;

public class BaseData<T> : IBaseData<T> where T : class
{
    protected readonly string _collectionName;
    protected readonly IMongoCollection<T> _collection;

    public BaseData(IMongoDbConnection mongoDbConnection, string collectionName)
    {
        _collectionName = collectionName;
        _collection = mongoDbConnection.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAll()
    {
        var results = await _collection.FindAsync(new BsonDocument());

        return results.ToList();
    }

    public async Task<T> GetOne(Expression<Func<T, bool>> expression)
    {
        var item = await _collection.FindAsync(expression);

        return (T)item;
    }

    public async Task CreateOne(T item)
    {
        try
        {
            await _collection.InsertOneAsync(item);
        }
        catch (Exception ex)
        {
        }
    }

    public async Task UpdateOne(Expression<Func<T, bool>> expression, UpdateDefinition<T> update)
    {
        await _collection.UpdateOneAsync(expression, update);
    }

    public async Task DeletePoll(Expression<Func<T, bool>> expression)
    {
        await _collection.DeleteOneAsync(expression);
    }
}
