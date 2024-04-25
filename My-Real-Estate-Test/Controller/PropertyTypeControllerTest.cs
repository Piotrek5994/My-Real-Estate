using Core.Command.Update;
using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Core.ModelDto;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;
using ZstdSharp.Unsafe;
public class PropertyTypeControllerTest
{
    private readonly Mock<IPropertyTypeService> _propertyTypeServiceMock;
    private readonly PropertyTypeController _propertyTypeController;

    public PropertyTypeControllerTest()
    {
        _propertyTypeServiceMock = new Mock<IPropertyTypeService>();
        _propertyTypeController = new PropertyTypeController(_propertyTypeServiceMock.Object);
    }

    [Fact]
    public async Task GetPropertyType_ReturnJsonResult()
    {
        //Arrange
        var propertyTypeFilter = new PropertyTypeFilter();
        var expectedPropertyType = new PropertyTypeDto();
        _propertyTypeServiceMock.Setup(s => s.GetPropertyTypeDto(It.IsAny<PropertyTypeFilter>()))
                                .ReturnsAsync(new List<PropertyTypeDto> { expectedPropertyType });
        //Act
        var controller = await _propertyTypeController.GetPropertyType(propertyTypeFilter);
        //Assert
        var jsonResult = Assert.IsType<JsonResult>(controller);
        Assert.NotNull(jsonResult.Value);
        var json = JsonSerializer.Serialize(jsonResult.Value);
        var deserializedResult = JsonSerializer.Deserialize<Dictionary<string,List<PropertyTypeDto>>>(json);
        Assert.NotNull(deserializedResult.Values);
        Assert.True(deserializedResult.ContainsKey("Result"));
        var propertyType = deserializedResult["Result"];
        Assert.NotNull(propertyType);
        Assert.Single(propertyType);
    }
    [Fact]
    public async Task GetPropertyType_ReturnNotFound_WithMessage()
    {
        //Arrange
        var propertyTypeFilter = new PropertyTypeFilter();
        var expectedPropertyType = new PropertyTypeDto();
        _propertyTypeServiceMock.Setup(s => s.GetPropertyTypeDto(It.IsAny<PropertyTypeFilter>()))
                                .ReturnsAsync((List<PropertyTypeDto>)null);
        //Act
        var controller = await _propertyTypeController.GetPropertyType(propertyTypeFilter);

        //Assert
        var jsonResult = Assert.IsType<NotFoundObjectResult>(controller);
        Assert.NotNull(jsonResult.Value);
        var deserializedResult = JsonSerializer.Deserialize<Dictionary<string,string>>(JsonSerializer.Serialize(jsonResult.Value));
        Assert.NotNull(deserializedResult.Values);
        string message;
        deserializedResult.TryGetValue("Message", out message);
        Assert.Equal("PropertyType don't found.", message);
    }
    [Fact]
    public async Task CreatePropertyType_ReturnOkResult()
    {
        //Arrange
        var propertyDto = new CreatePropertyTypeDto
        {
            PropertyTypeName = "Apartments"
        };
        string propertyId = "some-property-id";
        string expectedPropertyTypeId = "some-property-type-id";
        _propertyTypeServiceMock.Setup(s => s.CreatePropertyTypeDto(It.IsAny<CreatePropertyTypeDto>(),propertyId))
                                .ReturnsAsync(expectedPropertyTypeId);

        //Act
        var controller = new PropertyTypeController(_propertyTypeServiceMock.Object);
        var result = await controller.CreatePropertyType(propertyDto, propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("PropertyTypeId"));
        Assert.Equal(expectedPropertyTypeId, dictionary["PropertyTypeId"]);
    }
    [Fact]
    public async Task CreatePropertyTyp_ReturnBadRequest_WithMessage()
    {
        //Arrange
        var propertyDto = new CreatePropertyTypeDto
        {
            PropertyTypeName = "Apartments"
        };
        string propertyId = "some-property-id";
        string expectedPropertyTypeId = null;
        _propertyTypeServiceMock.Setup(s => s.CreatePropertyTypeDto(It.IsAny<CreatePropertyTypeDto>(), propertyId))
                                .ReturnsAsync(expectedPropertyTypeId);

        //Act
        var controller = new PropertyTypeController(_propertyTypeServiceMock.Object);
        var result = await controller.CreatePropertyType(propertyDto, propertyId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,string>>(json);
        Assert.NotNull(dictionary);
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Create property type fail.", message);
    }
    [Fact]
    public async Task UpdatePropertyType_ReturnOkResult()
    {
        //Arrange
        var updatePropertyType = new UpdatePropertyType
        {
            PropertyTypeName = ""
        };
        string propertyId = "some-property-id";
        _propertyTypeServiceMock.Setup(s => s.UpdatePropertyType(It.IsAny<UpdatePropertyType>(),propertyId))
                                .ReturnsAsync(true);

        //Act
        var controller = new PropertyTypeController(_propertyTypeServiceMock.Object);
        var result = await controller.UpdatePropertyType(updatePropertyType, propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("PropertyTypeUpdate"));
        bool message;
        dictionary.TryGetValue("PropertyTypeUpdate", out message);
        Assert.Equal(true, message);
    }
    [Fact]
    public async Task UpdatePropertyType_ReturnBadRequest_WithMessage()
    {
        //Arrange
        var updatePropertyType = new UpdatePropertyType
        {
            PropertyTypeName = ""
        };
        string propertyId = "some-property-id";
        _propertyTypeServiceMock.Setup(s => s.UpdatePropertyType(It.IsAny<UpdatePropertyType>(), propertyId))
                                .ReturnsAsync(false);

        //Act
        var controller = new PropertyTypeController(_propertyTypeServiceMock.Object);
        var result = await controller.UpdatePropertyType(updatePropertyType, propertyId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Update property type fail.", message);
    }
}
