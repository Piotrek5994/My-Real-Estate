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

        //Act

        //Assert
    }
    [Fact]
    public async Task Login_BadRequestResult_WithSecoundErrorMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task RefresToken_OkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task RefresToken_BadRequest_WithEmptyString_AndErrorMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task RefresToken_BadRequest_WithNullResponse_AndErrorMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task ChangeRole_OkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task ChangeRole_BadRequestResult_ByNotCorrectRole_WithErrorMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task ChangeRole_BadRequestResult_WithNullResponse_AndErrorMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task ChangePassword_OkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task ChamgePassowrd_OkResult_WithFalseResponse()
    {
        //Arrange

        //Act

        //Assert
    }
}

