using Core.Command.Update;
using Core.CommandDto;
using Core.Filter;
using Core.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;
public class AddressControllerTest
{
    private readonly Mock<IAddressService> _addressServiceMock;
    private readonly AddressController _addressController;
    public AddressControllerTest()
    {
        _addressServiceMock = new Mock<IAddressService>();
        _addressController = new AddressController(_addressServiceMock.Object);
    }
    [Fact]
    public async Task GetAddress_WithSuccessResult()
    {
        //Arrange
        var addressFilter = new AddressFilter();
        var expectedAddress = new AddressDto();
        _addressServiceMock.Setup(s => s.GetAddressDto(It.IsAny<AddressFilter>()))
                           .ReturnsAsync(new List<AddressDto> { expectedAddress });
        //Act
        var actionResult = await _addressController.GetAddress(addressFilter);

        //Assert
        var jsonResult = Assert.IsType<JsonResult>(actionResult);
        Assert.NotNull(jsonResult.Value);
        var json = JsonSerializer.Serialize(jsonResult.Value);
        var deserializedResult = JsonSerializer.Deserialize<Dictionary<string, List<AddressDto>>>(json);
        Assert.NotNull(deserializedResult);
        Assert.True(deserializedResult.ContainsKey("Result"));
        var address = deserializedResult["Result"];
        Assert.NotNull(address);
        Assert.Single(address);
    }
    [Fact]
    public async Task GetAddress_WithNotFoundResult_AndInformationMessage()
    {
        //Arrange
        List<AddressDto> simulate = null;
        var addressFilter = new AddressFilter();
        var expectedAddress = new AddressDto();
        _addressServiceMock.Setup(s => s.GetAddressDto(It.IsAny<AddressFilter>()))
                           .ReturnsAsync(simulate);
        //Act
        var actionResult = await _addressController.GetAddress(addressFilter);

        //Assert
        var jsonResult = Assert.IsType<NotFoundObjectResult>(actionResult);
        Assert.NotNull(jsonResult.Value);
        var json = JsonSerializer.Serialize(jsonResult.Value);
        var deserializedResult = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(deserializedResult);
        Assert.True(deserializedResult.ContainsKey("Message"));
        string message;
        deserializedResult.TryGetValue("Message", out message);
        Assert.Equal("Address or Addresses not found", message);

    }
    [Fact]
    public async Task CreateAddress_WithSuccesIdResult()
    {
        //Arrange
        var addressDto = new CreateAddressDto
        {
            Country = "Poland",
            Region = "Małopolskie",
            ZipCode = "31-550",
            City = "Kraków",
            StreetName = "Kolorowa",
            StreetNumber = "12",
            PropertyId = "65f82029010a574fe727cbc4"
        };
        string expecteAddressId = "Some-Address-Id";

        _addressServiceMock.Setup(s => s.CreateAddressDto(It.IsAny<CreateAddressDto>()))
                           .ReturnsAsync(expecteAddressId);

        //Act
        var controller = new AddressController(_addressServiceMock.Object);
        var result = await controller.CreateAddress(addressDto);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("AddressId"));
        Assert.Equal(expecteAddressId, dictionary["AddressId"]);

    }
    [Fact]
    public async Task CreateAddress_WithBadRequestResult_WithErrorMessage()
    {
        //Arrange
        var addressDto = new CreateAddressDto
        {
            Country = "Poland",
            Region = "Małopolskie",
            ZipCode = "31-550",
            City = "Kraków",
            StreetName = "Kolorowa",
            StreetNumber = "12",
            PropertyId = "65f82029010a574fe727cbc4"
        };
        string expecteAddressId = null;

        _addressServiceMock.Setup(s => s.CreateAddressDto(It.IsAny<CreateAddressDto>()))
                           .ReturnsAsync(expecteAddressId);

        //Act
        var controller = new AddressController(_addressServiceMock.Object);
        var result = await controller.CreateAddress(addressDto);

        //Assert
        var okResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        Assert.Equal("Create address fail", dictionary["Message"]);
    }
    [Fact]
    public async Task UpdateAddress_WithSuccesResult()
    {
        //Arrange
        var updateAddress = new UpdateAddress
        {
            Country = "USA",
            Region = "Florida",
            ZipCode = "33101",
            City = "Miami",
            StreetName = "Lincoln Road",
            StreetNumber = "206"
        };
        string addressId = "Some-Address-Id";
        bool expectedResult = true;

        //Act
        var controller = _addressServiceMock.Setup(s => s.UpdateAddress(updateAddress,addressId))
                                            .ReturnsAsync(expectedResult);
        var result = await _addressController.UpdateAddress(updateAddress, addressId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,bool>>(json);
        bool message;
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Result"));
        dictionary.TryGetValue("Result", out message);
        Assert.Equal(expectedResult,message);
    }
    [Fact]
    public async Task UpdateAddress_WithBadReguestResult_WithErrorMessage()
    {
        //Arrange
        var updateAddress = new UpdateAddress
        {
            Country = "USA",
            Region = "Florida",
            ZipCode = "33101",
            City = "Miami",
            StreetName = "Lincoln Road",
            StreetNumber = "206"
        };
        string addressId = "Some-Address-Id";
        bool expectedResult = false;

        //Act
        var controller = _addressServiceMock.Setup(s => s.UpdateAddress(updateAddress, addressId))
                                            .ReturnsAsync(expectedResult);
        var result = await _addressController.UpdateAddress(updateAddress, addressId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        string message;
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Address update fail", message);
    }
    [Fact]
    public async Task DeleteAddress_WithSuccessResult()
    {
        //Arrange
        string addressId = "Some-Address-Id";
        _addressServiceMock.Setup(s => s.DeleteAddress(addressId))
                           .ReturnsAsync(true);

        //Act
        var result = await _addressController.DeleteAddress(addressId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Result"));
        Assert.True(dictionary["Result"]);
    }
    [Fact]
    public async Task DeleteAddress_WithBadRequestResutl()
    {
        //Arrange
        string addressId = "Some-Address-Id";
        _addressServiceMock.Setup(s => s.DeleteAddress(addressId))
                           .ReturnsAsync(false);

        //Act
        var result = await _addressController.DeleteAddress(addressId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        string message;
        Assert.True(dictionary.ContainsKey("Message"));
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Address does not exist.", message);
    }
}

