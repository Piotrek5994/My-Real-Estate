using Core.Filter;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;

public class User
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _userControllerMock;
    public User()
    {
        _userServiceMock = new Mock<IUserService>();
        _userControllerMock = new UserController(_userServiceMock.Object);
    }
    [Fact]
    public async Task GetUser_ReturnsJsonResult_WithUser()
    {
        // Arrange specific to this test
        var userFilter = new UserFilter();
        var expectedUser = new UserDto(); // Populate as needed for comparison
        _userServiceMock.Setup(s => s.GetUserDto(It.IsAny<UserFilter>()))
                        .ReturnsAsync(new List<UserDto> { expectedUser });

        // Act
        var actionResult = await _userControllerMock.GetUser(userFilter);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(actionResult);
        Assert.NotNull(jsonResult.Value);

        var json = JsonSerializer.Serialize(jsonResult.Value);
        var deserializedResult = JsonSerializer.Deserialize<Dictionary<string, List<UserDto>>>(json);

        Assert.NotNull(deserializedResult);
        Assert.True(deserializedResult.ContainsKey("result"));
        var users = deserializedResult["result"];
        Assert.NotNull(users);
        Assert.Single(users);
    }
    [Fact]
    public async Task GetUser_ReturnNotFound_WithErrorMessage()
    {
        // Arrange
        var userFilter = new UserFilter();
        _userServiceMock.Setup(s => s.GetUserDto(It.IsAny<UserFilter>()))
                              .ReturnsAsync((List<UserDto>)null);
        var controller = new UserController(_userServiceMock.Object);

        // Act
        var result = await controller.GetUser(userFilter);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);

        var resultValue = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(notFoundResult.Value));
        Assert.NotNull(resultValue);
        string message;
        resultValue.TryGetValue("message", out message);
        Assert.Equal("User or Users don't found.", message);
    }
}

