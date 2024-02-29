using Infrastracture.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Db
{
    public class MongoDbContext
    {
        private readonly MongoDbSettings _mongoSettings;

        public MongoDbContext(IOptions<MongoDbSettings> mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionUri);
            var mongoDb = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);

        }
    }
}
