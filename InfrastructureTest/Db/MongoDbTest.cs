using Core.Model;
using Infrastracture.Settings;
using Infrastructure.Db;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;

namespace InfrastructureTest
{
    public class MongoDbContextTests
    {
        private MongoDbContext _mongoDbContext;

        [SetUp]
        public void Setup()
        {
            var mongoDbSettings = new MongoDbSettings
            {
                ConnectionUri = "mongodb+srv://piotrek5994:usgNgqaDcZimqGVi@piotrek.ybmunwo.mongodb.net/",
                DatabaseName = "My_Real_Estate"
            };

            var options = Options.Create(mongoDbSettings);

            _mongoDbContext = new MongoDbContext(options);
        }

        [Test]
        public void GetCollection_CheckingTheExistence()
        {
            // Arrange
            HashSet<string> collectionNames = new HashSet<string>
            {
                "Address",
                "Avatar",
                "Features",
                "Payment",
                "Photo",
                "Property",
                "PropertyType",
                "User"
            };

            // Act & Assert
            foreach (var name in collectionNames)
            {
                // Act
                var exists = _mongoDbContext.CollectionExists(name);

                // Assert
                Assert.IsTrue(exists, $"Collection '{name}' does not exist.");
            }
        }
    }
}
