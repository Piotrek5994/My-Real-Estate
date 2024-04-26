using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text;
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
        string userId = "Some-User-Id";
        byte[] photoBytes = Encoding.UTF8.GetBytes("Some-Photo");
        Stream bytePhoto = new MemoryStream(photoBytes);
        _photoServiceMock.Setup(s => s.GetAvatar(userId))
                         .ReturnsAsync(bytePhoto);

        // Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.GetAvatarPhoto(userId);

        //Assert
        var fileResult = Assert.IsType<FileContentResult>(result);
        string resultContent = Encoding.UTF8.GetString(fileResult.FileContents); 
        Assert.Equal("Some-Photo", resultContent);
    }
    [Fact]
    public async Task GetAvatarPhoto_ReturnBadRequestResult_WithMessage()
    {
        //Arrange
        string userId = "Some-User-Id";
        Stream bytePhoto = null;
        _photoServiceMock.Setup(s => s.GetAvatar(userId))
                         .ReturnsAsync(bytePhoto);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.GetAvatarPhoto(userId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
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

