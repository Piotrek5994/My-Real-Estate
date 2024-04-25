using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Core.ModelDto;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;
public class PropertyControllerTest
{
    private readonly Mock<IPropertyService> _propertyServiceMock;
    private readonly PropertyController _propertyController;

    public PropertyControllerTest()
    {
        _propertyServiceMock = new Mock<IPropertyService>();
        _propertyController = new PropertyController(_propertyServiceMock.Object);
    }

    [Fact]
    public async Task GetProperty_ReturnJsonResult()
    {
        //Arrange
        var propertyFilter = new PropertyFilter();
        var expectedProperty = new PropertyDto();
        _propertyServiceMock.Setup(s => s.GetPropertyDto(It.IsAny<PropertyFilter>()))
                             .ReturnsAsync(new List<PropertyDto> { expectedProperty });
        //Act
        var actionResult = await _propertyController.GetProperty(propertyFilter);

        //Assert
        var jsonResult = Assert.IsType<JsonResult>(actionResult);
        Assert.NotNull(jsonResult.Value);

        var json = JsonSerializer.Serialize(jsonResult.Value);
        var deserializedResult = JsonSerializer.Deserialize<Dictionary<string, List<PropertyDto>>>(json);
        Assert.NotNull(deserializedResult);
        Assert.True(deserializedResult.ContainsKey("Result"));
        var property = deserializedResult["Result"];
        Assert.NotNull(property);
        Assert.Single(property);
    }
    [Fact]
    public async Task GetProperty_ReturnNotFound_WithErrorMessage()
    {
        //Arrange
        var propertyFilter = new PropertyFilter();
        _propertyServiceMock.Setup(s => s.GetPropertyDto(It.IsAny<PropertyFilter>()))
                             .ReturnsAsync((List<PropertyDto>)null);
        var controller = new PropertyController(_propertyServiceMock.Object);

        //Act
        var result = await controller.GetProperty(propertyFilter);

        //Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);

        var resultValue = JsonSerializer.Deserialize<Dictionary<string,string>>(JsonSerializer.Serialize(notFoundResult.Value));
        Assert.NotNull(resultValue);
        string message;
        resultValue.TryGetValue("Message", out message);
        Assert.Equal("Property of Propertys don't found", message);
    }
    [Fact]
    public async Task CreateProperty_ReturnOk_WithPropertyId()
    {
        //Arrange
        var propertyDto = new CreatePropertyDto
        {
            Name = "Ocean View Apartment",
            Description = "A luxurious apartment with 3 bedrooms overlooking the ocean. Features include a fully equipped kitchen, spacious living room, and access to a private beach.",
            Price = 1500.00m,
            Size = 120,
            NumberOfRooms = 3,
            NumberOfPeople = 6,
            RentStart = "2024-03-18",
            RentEnd = "2024-03-18",
            State = 0,
            UserId = "user123"
        };
        string expectedUserId = "someUserId";

        _propertyServiceMock.Setup(s => s.CreatePropertyDto(It.IsAny<CreatePropertyDto>())).ReturnsAsync(expectedUserId);
        
        //Act
        var contoller = new PropertyController(_propertyServiceMock.Object);
        var result = await contoller.CreateProperty(propertyDto);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("PropertyId"));
        Assert.Equal(expectedUserId,dictionary["PropertyId"]);
    }
    [Fact]
    public async Task CreateProperty_ReturnBadRequest_WithErrorMessage()
    {
        //Arrange
        var propertyDto = new CreatePropertyDto
        {
            Name = "Ocean View Apartment",
            Description = "A luxurious apartment with 3 bedrooms overlooking the ocean. Features include a fully equipped kitchen, spacious living room, and access to a private beach.",
            Price = 1500.00m,
            Size = 120,
            NumberOfRooms = 3,
            NumberOfPeople = 6,
            RentStart = "2024-03-18",
            RentEnd = "2024-03-18",
            State = 0,
            UserId = "user123"
        };
        string? expectedResult = null;

        _propertyServiceMock.Setup(s => s.CreatePropertyDto(propertyDto)).ReturnsAsync(expectedResult);

        //Act
        var controller = new PropertyController(_propertyServiceMock.Object);
        var result = await controller.CreateProperty(propertyDto);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Create property fail", dictionary["Message"]);
    }
    [Fact]
    public async Task UpdateProperty_ReturnOk_WithResultTrue()
    {
        //Arrange
        var updateProperty = new UpdateProperty
        {
            Name = "",
            Description = "",
            Price = null,
            Size = null,
            NumberOfRooms = null,
            NumberOfPeople = null,
            RentStart = null,
            RentEnd = null,
            State = null
        };
        string propertyId = "some-property-Id";
        bool expectedResult = true;
        _propertyServiceMock.Setup(s => s.UpdateProperty(updateProperty,propertyId)).ReturnsAsync(expectedResult);

        //Act
        var result = await _propertyController.UpdateProperty(updateProperty, propertyId);
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,bool>>(json);
        bool message;
        dictionary.TryGetValue("Result", out message);
        Assert.True(message);
    }
    [Fact]
    public async Task UpdateProperty_ReturnBadRequest_WithErrorMessage()
    {
        //Arrange
        var updateProperty = new UpdateProperty
        {
            Name = "",
            Description = "",
            Price = null,
            Size = null,
            NumberOfRooms = null,
            NumberOfPeople = null,
            RentStart = null,
            RentEnd = null,
            State = null
        };
        string propertyId = "some-property-Id";
        bool expectedResult = false;
        string expectedMessage = "Property update fail";
         _propertyServiceMock.Setup(s => s.UpdateProperty(updateProperty, propertyId)).ReturnsAsync(expectedResult);
        
        //Act
        var result = await _propertyController.UpdateProperty(updateProperty, propertyId);
        
        //Assert
        var resultBadRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(resultBadRequest.Value);
        var json = JsonSerializer.Serialize(resultBadRequest.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal(expectedMessage,message);
    }
    [Fact]
    public async Task DeleteProperty_ReturnOk_WithResultTrue()
    {
        //Arrange
        string propertyId = "some-propert-Id";
        _propertyServiceMock.Setup(s => s.DeleteProperty(propertyId)).ReturnsAsync(true);

        //Act
        var result = await _propertyController.DeleteProperty(propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Result"));
        Assert.True(dictionary["Result"]);
    }
    [Fact]
    public async Task DeletePropert_ReturnBadRequest_WithErrorMessage()
    {
        //Arrange
        string propertyId = "some-propert-Id";
        _propertyServiceMock.Setup(s => s.DeleteProperty(propertyId)).ReturnsAsync(false);

        //Act
        var result = await _propertyController.DeleteProperty(propertyId);
        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        Assert.Equal("Property does not exist.", dictionary["Message"]);
    }
}

