using Infrastracture.Settings;
using Infrastructure.Db;
using Microsoft.Extensions.Options;
using Xunit;

namespace Infrastructure_IntegrationTest.Db
{
    public class MongoDbContextTests
    {
        private readonly MongoDbContext _mongoDbContext;

        public MongoDbContextTests()
        {
            var mongoDbSettings = new MongoDbSettings
            {
                ConnectionUri = "mongodb+srv://piotrek5994:usgNgqaDcZimqGVi@piotrek.ybmunwo.mongodb.net/",
                DatabaseName = "My_Real_Estate"
            };

            var options = Options.Create(mongoDbSettings);

            _mongoDbContext = new MongoDbContext(options);
        }

        [Fact]
        public void GetCollection_CheckingTheExistence()
        {
            // Arrange
            var collectionNames = new[]
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
                Assert.True(exists, $"Collection '{name}' does not exist.");
            }
        }
    }
}