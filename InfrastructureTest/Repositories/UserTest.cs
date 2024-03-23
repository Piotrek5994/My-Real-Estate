using Core.Commend.Create;
using Core.IRepositories;
using Infrastracture.Repositories;
using Infrastracture.Settings;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Infrastructure_IntegrationTest.Repositories;

public class UserTest
{
    private readonly Mock<MongoDbContext> _mockContext;
    private readonly Mock<ILogger<UserRepository>> _mockLogger;

    public UserTest()
    {
        _mockLogger = new Mock<ILogger<UserRepository>>();
        var mockOptions = new Mock<IOptions<MongoDbSettings>>();
        var mockSettings = new MongoDbSettings
        {
            ConnectionUri = "mongodb+srv://piotrek5994:usgNgqaDcZimqGVi@piotrek.ybmunwo.mongodb.net/",
            DatabaseName = "My_Real_Estate"
        };
        mockOptions.Setup(x => x.Value).Returns(mockSettings);
        _mockContext = new Mock<MongoDbContext>(mockOptions.Object);
    }

    [Fact]
    public async Task CreateUser_UserExists_ReturnsErrorMessage()
    {
        // Arrange
        var repository = new UserRepository(_mockContext.Object, _mockLogger.Object);
        var user = new CreateUser { Email = "john.doe@example.com" };

        // Act
        var result = await repository.CreateUser(user);

        // Assert
        Assert.Equal("The user with the provided email address is already registered.", result);
    }
    [Fact]
    public async Task CreateAdmin_AdminExists_ReturnErrorMessage()
    {
        var repository = new UserRepository(_mockContext.Object, _mockLogger.Object);
        var user = new CreateUser { Email = "john.doe@example.com" };

        // Act
        var result = await repository.CreateAdmin(user);

        // Assert
        Assert.Equal("The user with the provided email address is already registered.", result);
    }
}
