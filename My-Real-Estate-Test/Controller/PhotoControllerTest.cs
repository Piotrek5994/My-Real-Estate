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
        _photoServiceMock.Setup(s => s.UploadPhoto(It.IsAny<IFormFile>(), userId, "Avatar"))
                                .ReturnsAsync(collectionId);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UploadAvatarPhoto(formFile.Object, userId);

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
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UploadAvatarPhoto(formFile.Object, userId);

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
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UploadAvatarPhoto(formFile.Object, userId);

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
        string userId = "Some-User-Id";
        string response = "Avatar changed successfully";
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
        _photoServiceMock.Setup(s => s.ChangePhoto(It.IsAny<IFormFile>(), userId, "ChangeAvatar"))
                                .ReturnsAsync(response);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UpdateAvatarPhoto(formFile.Object, userId);

        //Assert
        var okReslt = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okReslt.Value);
        var json = JsonSerializer.Serialize(okReslt.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("ChangeAvatar"));
        string message;
        dictionary.TryGetValue("ChangeAvatar", out message);
        Assert.Equal(response, message);
    }
    [Fact]
    public async Task DeleteAvatarPhoto_ReturnOkResult()
    {
        //Arrange
        string userId = "Some-User-Id";
        _photoServiceMock.Setup(s => s.DeleteAvatar(userId));

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.DeleteAvatarPhoto(userId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Success"));
    }
    [Fact]
    public async Task GetPropertyPhoto_ReturnOkResult()
    {
        //Arrange
        string photoId = "Some-Photo-Id";
        string propertyId = "Some-Property-Id";
        byte[] photoBytes = Encoding.UTF8.GetBytes("Some-Photo");
        Stream bytePhoto = new MemoryStream(photoBytes);
        _photoServiceMock.Setup(s => s.GetPropertyPhotos(propertyId, photoId))
                         .ReturnsAsync(bytePhoto);

        // Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.GetPropertyPhoto(propertyId, photoId);

        //Assert
        var fileResult = Assert.IsType<FileContentResult>(result);
        Assert.NotNull(fileResult);
        Assert.Equal("image/jpeg", fileResult.ContentType);
        string resultContent = Encoding.UTF8.GetString(fileResult.FileContents);
        Assert.Equal("Some-Photo", resultContent);
    }
    [Fact]
    public async Task GetPropertyPhoto_ReturnNotFoundResult_WithMessage()
    {
        //Arrange
        string photoId = "Some-Photo-Id";
        string propertyId = "Some-Property-Id";
        Stream expectedResult = null;
        string expectedErrorMessage = "The property does not exist or does not have an photo.";
        byte[] photoBytes = Encoding.UTF8.GetBytes("Some-Photo");
        Stream bytePhoto = new MemoryStream(photoBytes);
        _photoServiceMock.Setup(s => s.GetPropertyPhotos(propertyId, photoId))
                         .ReturnsAsync(expectedResult);

        // Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.GetPropertyPhoto(propertyId, photoId);

        //Assert
        var badRequestResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary.Values);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(expectedErrorMessage, message);
    }
    [Fact]
    public async Task CreatePropertyPhoto_ReturnOkResult()
    {
        //Arrange
        string propertyId = "Property-Id";
        var memoryStream1 = new MemoryStream(Encoding.UTF8.GetBytes("Fake file"));
        var memoryStream2 = new MemoryStream(Encoding.UTF8.GetBytes("Fake file 2"));
        var formFile1 = new FormFile(memoryStream1, 0, memoryStream1.Length, "file1", "test.pdf");
        var formFile2 = new FormFile(memoryStream2, 0, memoryStream2.Length, "file2", "test2.pdf");

        var formFiles = new FormFileCollection { formFile1, formFile2 };

        var photoIds = new List<string> { "Photo1", "Photo2" };
        _photoServiceMock.Setup(s => s.UploadsPropertyPhotos(It.IsAny<IFormFileCollection>(), propertyId))
                         .ReturnsAsync(photoIds);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UploadPropertyPhotos(formFiles, propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }
    [Fact]
    public async Task CreatePropertyPhoto_ReturnBadRequestResult_WithMessage()
    {
        //Arrange
        string propertyId = "Property-Id";
        var memoryStream1 = new MemoryStream(Encoding.UTF8.GetBytes(""));
        var memoryStream2 = new MemoryStream(Encoding.UTF8.GetBytes(""));
        var formFile1 = new FormFile(memoryStream1, 0, memoryStream1.Length, "file1", "test.pdf");
        var formFile2 = new FormFile(memoryStream2, 0, memoryStream2.Length, "file2", "test2.pdf");

        var formFiles = new FormFileCollection { formFile1, formFile2 };
        var photoIds = new List<string>();
        _photoServiceMock.Setup(s => s.UploadsPropertyPhotos(It.IsAny<IFormFileCollection>(), propertyId))
                         .ReturnsAsync(photoIds);
        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UploadPropertyPhotos(formFiles, propertyId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary.Values);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Create photo for property fail", message);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    [Fact]
    public async Task UpdatePropertyPhoto_ReturnOkResult()
    {
        // Arrange
        string photoId = "Photo-Id";
        string propertyId = "Property-Id";
        var fakeFileContents = Encoding.UTF8.GetBytes("Fake file content");
        var memoryStream = new MemoryStream(fakeFileContents);
        var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file1", "test.pdf");

        var response = new List<string> { "Change propery photo success" };

        _photoServiceMock.Setup(s => s.ChangePropertyPhoto(It.IsAny<IFormFile>(), photoId, propertyId))
                         .ReturnsAsync(response);

        // Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.UpdatePropertyPhotos(formFile, photoId, propertyId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
        Assert.True(dictionary.ContainsKey("ChangePropertyPhoto"));
        var messages = dictionary["ChangePropertyPhoto"];
        Assert.NotNull(messages);
        Assert.Single(messages);
        Assert.Equal("Change propery photo success", messages[0]);
    }
    [Fact]
    public async Task DeletePropertyPhoto_ReturnOkResult()
    {
        //Arrange
        List<string> photoIds = new List<string> { "First-Photo-Id", "Secound-Photo-Id" };
        string propertyId = "Some-Property-Id";
        _photoServiceMock.Setup(s => s.DeletePropertyPhotos(photoIds, propertyId))
                         .ReturnsAsync(true);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.DeletePropertyPhotos(photoIds,propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,bool>>(json);
        Assert.True(dictionary.ContainsKey("Success"));
        bool message = dictionary["Success"];
        Assert.True(message);
    }
    [Fact]
    public async Task DeletePropertyPhoto_ReturnOkResult_WithFalse()
    {
        //Arrange
        List<string> photoIds = new List<string> { "First-Photo-Id", "Secound-Photo-Id" };
        string propertyId = "Some-Property-Id";
        _photoServiceMock.Setup(s => s.DeletePropertyPhotos(photoIds, propertyId))
                         .ReturnsAsync(false);

        //Act
        var controller = new PhotoController(_photoServiceMock.Object);
        var result = await controller.DeletePropertyPhotos(photoIds, propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.True(dictionary.ContainsKey("Success"));
        bool message = dictionary["Success"];
        Assert.False(message);
    }
}

