using Core.Commend.Update;
using Infrastracture.Repositories;
using Infrastracture.Settings;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;

namespace Infrastructure_Test.Repositories;

public class UserTest
{
    private readonly Mock<MongoDbContext> _mockContext;
    private readonly Mock<ILogger<UserRepository>> _mockLogger;
    private readonly UserRepository _userRepository;
    private readonly Mock<IMongoDatabase> _mockDatabase;
    private readonly Mock<IMongoCollection<BsonDocument>> _mockCollection;
    private readonly Mock<IOptions<MongoDbSettings>> _mockSettings;
    public UserTest()
    {
        // Mocking the database and collection
        _mockLogger = new Mock<ILogger<UserRepository>>();
        _mockDatabase = new Mock<IMongoDatabase>();
        _mockCollection = new Mock<IMongoCollection<BsonDocument>>();
        _mockSettings = new Mock<IOptions<MongoDbSettings>>();

        // Setup mock settings
        var settings = new MongoDbSettings
        {
            ConnectionUri = "mongodb+srv://piotrek5994:usgNgqaDcZimqGVi@piotrek.ybmunwo.mongodb.net/",
            DatabaseName = "My_Real_Estate"
        };
        _mockSettings.Setup(s => s.Value).Returns(settings);

        // Mocking the MongoDbContext to return the mock database and setup the database to return the mock collection
        var mockContext = new Mock<MongoDbContext>(_mockSettings.Object);
        mockContext.Setup(c => c.GetCollection<BsonDocument>(It.IsAny<string>())).Returns(_mockCollection.Object);

        // Instantiating the UserRepository with the mocked MongoDbContext
        var repository = new UserRepository(_mockContext.Object, _mockLogger.Object);
    }
}
