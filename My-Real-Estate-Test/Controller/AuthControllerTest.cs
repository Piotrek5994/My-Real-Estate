using Core.Commend.Create;
using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;
public class AuthControllerTest
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _authController;

    public AuthControllerTest()
    {
        _authServiceMock = new Mock<IAuthService>();
        _authController = new AuthController(_authServiceMock.Object);
    }
    [Fact]
    public async Task Login_OkResult()
    {
        //Arrange
        var login = new CreateLogin
        {
            Email = "piot4444@gmail.com",
            Password = "11111"
        };
        string expectedToken = "BardzoTajemnyToken";
        _authServiceMock.Setup(s => s.Login(login)).ReturnsAsync(expectedToken);

        //Act
        var actionResult = await _authController.Login(login);

        //Assert
        var jsonResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.NotNull(jsonResult.Value);
        var json = JsonSerializer.Serialize(jsonResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Token"));
        string token;
        dictionary.TryGetValue("Token", out token);
        Assert.Equal(expectedToken, token);
    }
    [Fact]
    public async Task Login_BadRequestResult_WithFirstErrorMessage()
    {
        //Arrange
        var login = new CreateLogin
        {
            Email = "piot4444@gmail.com",
            Password = "11111"
        };
        string expectedMessage = "Authentication failed. User not found.";
        _authServiceMock.Setup(s => s.Login(login)).ReturnsAsync(expectedMessage);

        //Act
        var actionResult = await _authController.Login(login);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(expectedMessage, message);
    }
    [Fact]
    public async Task Login_BadRequestResult_WithSecoundErrorMessage()
    {
        //Arrange
        var login = new CreateLogin
        {
            Email = "piot4444@gmail.com",
            Password = "11111"
        };
        string expectedMessage = "Authentication failed. Password incorrect.";
        _authServiceMock.Setup(s => s.Login(login)).ReturnsAsync(expectedMessage);

        //Act
        var actionResult = await _authController.Login(login);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(expectedMessage, message);
    }
    [Fact]
    public async Task RefresToken_OkResult()
    {
        //Arrange
        string token = "Token";
        string expectedToken = "Token";
        _authServiceMock.Setup(s => s.RefreshToken(token)).ReturnsAsync(expectedToken);

        //Act
        var actionResult = await _authController.RefresToken(token);

        //Assert
        var jsonResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.NotNull(jsonResult.Value);
        var json = JsonSerializer.Serialize(jsonResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Token"));
        string token2;
        dictionary.TryGetValue("Token", out token2);
        Assert.Equal(expectedToken, token2);
    }
    [Fact]
    public async Task RefresToken_BadRequest_WithEmptyString_AndErrorMessage()
    {
        //Arrange
        string token = "";
        string expectedMessage = "Token is null or empty.";
        _authServiceMock.Setup(s => s.RefreshToken(token)).ReturnsAsync(expectedMessage);

        //Act
        var actionResult = await _authController.RefresToken(token);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(expectedMessage, message);
    }
    [Fact]
    public async Task RefresToken_BadRequest_WithNullResponse_AndErrorMessage()
    {
        //Arrange
        string token = "Token";
        string expectedMessage = null;
        string expectedResponse = "Invalid token sent.";
        _authServiceMock.Setup(s => s.RefreshToken(token)).ReturnsAsync(expectedMessage);

        //Act
        var actionResult = await _authController.RefresToken(token);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(expectedResponse, message);
    }
    [Fact]
    public async Task ChangeRole_OkResult()
    {
        // Arrange
        string userId = "Some-User-Id";
        string role = "User";
        bool expectedResult = true;
        _authServiceMock.Setup(s => s.UpdateUserRole(userId, role)).ReturnsAsync(expectedResult);

        // Act
        var actionResult = await _authController.ChangeRole(userId, role);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("ChangeRole"));
        Assert.Equal(role, dictionary["ChangeRole"].ToString());
    }
    [Fact]
    public async Task ChangeRole_BadRequestResult_ByNotCorrectRole_WithErrorMessage()
    {
        //Arrange
        string userId = "Some-User-Id";
        string role = "SuperAdmin";
        _authServiceMock.Setup(s => s.UpdateUserRole(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

        //Act
        var actionResult = await _authController.ChangeRole(userId, role);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        Assert.Contains("Invalid role", dictionary["Message"]);
    }
    [Fact]
    public async Task ChangeRole_BadRequestResult_WithNullResponse_AndErrorMessage()
    {
        //Arrange
        string userId = "Some-User-Id";
        string role = null;
        _authServiceMock.Setup(s => s.UpdateUserRole(userId, role)).ReturnsAsync(false);

        //Act
        var actionResult = await _authController.ChangeRole(userId, role);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        Assert.Contains("Incorrect role change for user", dictionary["Message"]);
    }
    [Fact]
    public async Task ChangePassword_OkResult()
    {
        //Arrange
        var password = new UpdatePassword { userId = "User1", OldPassword = "oldPass", NewPassword = "newPass" };
        _authServiceMock.Setup(s => s.ChangeUserPassword(password)).ReturnsAsync(true);

        //Act
        var actionResult = await _authController.ChangePassword(password);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("ChangePassword"));
        Assert.True(dictionary["ChangePassword"]);
    }
    [Fact]
    public async Task ChangePassword_OkResult_WithFalseResponse()
    {
        //Arrange
        var password = new UpdatePassword { userId = "User1", OldPassword = "oldPass", NewPassword = "newPass" };
        _authServiceMock.Setup(s => s.ChangeUserPassword(password)).ReturnsAsync(false);

        //Act
        var actionResult = await _authController.ChangePassword(password);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("ChangePassword"));
        Assert.False(dictionary["ChangePassword"]);
    }
}

