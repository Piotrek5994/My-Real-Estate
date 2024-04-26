using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;
public class PhotoControllerTest
{
    private readonly Mock<IPhotoService> _photoServiceMock;
    private readonly PhotoController _photoController;

    public PhotoControllerTest()
    {
        _photoServiceMock = new Mock<IPhotoService>();
        _photoController = new PhotoController(_photoServiceMock.Object);
    }
    [Fact]
    public async Task GetAvatarPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task GetAvatarPhoto_ReturnBadRequestResult_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task CreateAvatarPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task CreateAvatarPhoto_ReturnBadRequest_WithMessage_WhenUserIdIsNullOrEmpty()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task CreateAvatarPhoto_ReturnBadRequest_WithMessage_WhenResultIsNull()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task UpdateAvatarPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task DeleteAvatarPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task GetPropertyPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task GetPropertyPhoto_ReturnNotFoundResult_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task CreatePropertyPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task CreatePropertyPhoto_ReturnBadRequestResult_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task UpdatePropertyPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task DeletePropertyPhoto_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
}

