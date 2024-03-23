using NUnit.Framework;
using Infrastructure.Db;
using Microsoft.Extensions.Options;
using Infrastracture.Settings;
using MongoDB.Driver;

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
        public void GetCollection_ReturnUserCollection()
        {
            // Arrange
            string collectionName = "User";

            // Act
            var collection = _mongoDbContext.GetCollection<object>(collectionName);

            // Assert
            Assert.That(collection, Is.Not.Null);
        }
        [Test]
        public void GetCollection_ReturnsPropertyTypeCollection()
        {
            // Arrange
            string collectionName = "PropertyType";

            // Act 
            var collection = _mongoDbContext.GetCollection<object>(collectionName);

            //Assert
            Assert.True(collection != null);
        }
        [Test]
        public void GetCollection_ReturnsPropertyCollection()
        {
            // Arrange
            string collectionName = "Property";

            // Act
            var collection = _mongoDbContext.GetCollection<object>(collectionName);

            // Assert
            Assert.IsInstanceOf<IMongoCollection<object>>(collection);
        }
    }
}
