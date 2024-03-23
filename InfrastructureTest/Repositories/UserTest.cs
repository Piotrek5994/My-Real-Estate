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

    public UserTest()
    {
        var mockOptions = new Mock<IOptions<MongoDbSettings>>();
        var mockSettings = new MongoDbSettings {
            ConnectionUri = "mongodb+srv://piotrek5994:usgNgqaDcZimqGVi@piotrek.ybmunwo.mongodb.net/",
            DatabaseName = "My_Real_Estate"
        };
        mockOptions.Setup(x => x.Value).Returns(mockSettings);
        _mockContext = new Mock<MongoDbContext>(mockOptions.Object);
    }

    [Fact]
    public async Task CreateUser_Powodzenie()
    {
        // Arrange
        var repository = new UserRepository(_mockContext.Object, new Mock<ILogger<UserRepository>>().Object);
        var user = new CreateUser
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Gender = "Male",
            PESEL = "12345678901",
            Role = "User",
            Email = "jan.kowalski@example.com",
            Password = "password123",
            PhoneNumber = "123-456-789",
            Properties = new List<string> { "property1", "property2" },
            Payments = new List<string> { "payment1", "payment2" }
        };

        // Act
        var result = await repository.CreateUser(user);

        // Assert
        Assert.NotNull(result);
    }
}
