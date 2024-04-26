using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Http;
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
        Assert.NotNull(fileResult);
        Assert.Equal("image/jpeg", fileResult.ContentType);
        string resultContent = Encoding.UTF8.GetString(fileResult.FileContents); 
        Assert.Equal("Some-Photo", resultContent);
    }
    [Fact]
    public async Task GetAvatarPhoto_ReturnBadRequestResult_WithMessage()
    {
        //Arrange
        string userId = "Some-User-Id";
        string errorMessage = "The user does not exist or does not have an avatar.";
        Stream bytePhoto = null;
        _photoServiceMock.Setup(s => s.GetAvatar(userId))
                         .ReturnsAsync(bytePhoto);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.GetAvatarPhoto(userId);

        //Assert
        var badRequestResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(errorMessage, message);
    }
    [Fact]
    public async Task CreateAvatarPhoto_ReturnOkResult()
    {
        //Arrange
        string userId = "Some-User-Id";
        string collectionId = "Collection-Id";
        var formFile = new Mock<IFormFile>();
        var phisicalFile = new FileInfo("test.jpg");
        var memory = new MemoryStream();
        var writer = new StreamWriter(memory);
        writer.Write("Dumy immage data");
        writer.Flush();
        memory.Position = 0;
        var fileName = phisicalFile.Name;
        formFile.Setup(_ => _.FileName).Returns(fileName);
        formFile.Setup(_ => _.Length).Returns(memory.Length);
        formFile.Setup(_ => _.OpenReadStream()).Returns(memory);
        _photoServiceMock.Setup(s => s.UploadPhoto(It.IsAny<IFormFile>(),userId, "Avatar"))
                                .ReturnsAsync(collectionId);

        //Act
        var collection = new PhotoController(_photoServiceMock.Object);
        var result = await collection.UploadAvatarPhoto(formFile.Object,userId);

        //Assert
        var okReslt = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okReslt.Value);
        var json = JsonSerializer.Serialize(okReslt.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("AvatarId"));
        string message;
        dictionary.TryGetValue("AvatarId", out message);
        Assert.Equal(collectionId, message);
    }
    [Fact]
    public async Task CreateAvatarPhoto_ReturnBadRequest_WithMessage_WhenUserIdIsNullOrEmpty()
    {
        //Arrange
        string userId = "";
        string errorMessage = "User ID is required.";
        var formFile = new Mock<IFormFile>();
        var phisicalFile = new FileInfo("test.jpg");
        var memory = new MemoryStream();
        var writer = new StreamWriter(memory);
        writer.Write("Dumy immage data");
        writer.Flush();
        memory.Position = 0;
        var fileName = phisicalFile.Name;
        formFile.Setup(_ => _.FileName).Returns(fileName);
        formFile.Setup(_ => _.Length).Returns(memory.Length);
        formFile.Setup(_ => _.OpenReadStream()).Returns(memory);
        _photoServiceMock.Setup(s => s.UploadPhoto(It.IsAny<IFormFile>(), userId, "Avatar"))
                                .ReturnsAsync(errorMessage);

        //Act
        var collection = new PhotoController(_photoServiceMock.Object);
        var result = await collection.UploadAvatarPhoto(formFile.Object, userId);

        //Assert
        var badRequestReslt = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestReslt.Value);
        var json = JsonSerializer.Serialize(badRequestReslt.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(errorMessage, message);
    }
    [Fact]
    public async Task CreateAvatarPhoto_ReturnBadRequest_WithMessage_WhenResultIsNull()
    {
        //Arrange
        string userId = "Some-User-Id";
        string resultValue = null;
        string errorMessage = "Error loading image.";
        var formFile = new Mock<IFormFile>();
        var phisicalFile = new FileInfo("test.jpg");
        var memory = new MemoryStream();
        var writer = new StreamWriter(memory);
        writer.Write("Dumy immage data");
        writer.Flush();
        memory.Position = 0;
        var fileName = phisicalFile.Name;
        formFile.Setup(_ => _.FileName).Returns(fileName);
        formFile.Setup(_ => _.Length).Returns(memory.Length);
        formFile.Setup(_ => _.OpenReadStream()).Returns(memory);
        _photoServiceMock.Setup(s => s.UploadPhoto(It.IsAny<IFormFile>(), userId, "Avatar"))
                                .ReturnsAsync(resultValue);

        //Act
        var collection = new PhotoController(_photoServiceMock.Object);
        var result = await collection.UploadAvatarPhoto(formFile.Object, userId);

        //Assert
        var badRequestReslt = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestReslt.Value);
        var json = JsonSerializer.Serialize(badRequestReslt.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(errorMessage, message);
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

