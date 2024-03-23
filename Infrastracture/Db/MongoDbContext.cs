using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Infrastracture.Settings;
using MongoDB.Bson;

namespace Infrastructure.Db;

public class MongoDbContext
{
    public readonly MongoDbSettings _settings;
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionUri);
        _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
    }
    public virtual IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
    public bool CollectionExists(string name)
    {
        var filter = new BsonDocument("name", name);
        var collections = _database.ListCollections(new ListCollectionsOptions { Filter = filter });
        return collections.Any();
    }
}
