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

        //Act

        //Assert
    }
    [Fact]
    public async Task CreatePropertyTyp_ReturnBadRequest_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task UpdatePropertyType_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task UpdatePropertyType_ReturnBadRequest_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
}
