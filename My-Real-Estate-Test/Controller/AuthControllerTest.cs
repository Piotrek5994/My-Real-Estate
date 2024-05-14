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

    }
    [Fact]
    public async Task Login_BadRequestResult_WithFirstErrorMessage()
    {

    }
    [Fact]
    public async Task Login_BadRequestResult_WithSecoundErrorMessage()
    {

    }
    [Fact]
    public async Task RefresToken_OkResult()
    {

    }
    [Fact]
    public async Task RefresToken_BadRequest_WithEmptyString_AndErrorMessage()
    {

    }
    [Fact]
    public async Task RefresToken_BadRequest_WithNullResponse_AndErrorMessage()
    {

    }
    [Fact]
    public async Task ChangeRole_OkResult()
    {

    }
    [Fact]
    public async Task ChangeRole_BadRequestResult_ByNotCorrectRole_WithErrorMessage()
    {

    }
    [Fact]
    public async Task ChangeRole_BadRequestResult_WithNullResponse_AndErrorMessage()
    {

    }
    [Fact]
    public async Task ChangePassword_OkResult()
    {

    }
    [Fact]
    public async Task ChamgePassowrd_OkResult_WithFalseResponse()
    {

    }
}

