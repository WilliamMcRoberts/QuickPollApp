
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

    public async Task<List<T>> GetAllAsync()
    {
        var results = await _collection.FindAsync(new BsonDocument());

        return results.ToList();
    }

    public async Task<T> GetOneAsync(Expression<Func<T, bool>> expression)
    {
        var results = await _collection.FindAsync(expression);

        return results.FirstOrDefault();
    }

    public async Task CreateOneAsync(T item)
    {
        try
        {
            await _collection.InsertOneAsync(item);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync("Error adding item");
        }
    }

    public Task ReplaceOneAsync(string itemId, T item, string field = "Id")
    {
        var filter = Builders<T>.Filter.Eq(field, itemId);

        return _collection.ReplaceOneAsync(filter, item, new ReplaceOptions { IsUpsert = true });
    }

    public async Task UpdateOneAsync(Expression<Func<T, bool>> expression, UpdateDefinition<T> update)
    {
        await _collection.UpdateOneAsync(expression, update);
    }

    public async Task DeletePollAsync(Expression<Func<T, bool>> expression)
    {
        await _collection.DeleteOneAsync(expression);
    }
}
