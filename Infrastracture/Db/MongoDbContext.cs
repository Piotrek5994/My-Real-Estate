using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Infrastracture.Settings;

namespace Infrastructure.Db
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionUri);
            _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
