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
        //Act
        //Assert
    }
    [Fact]
    public async Task CreateProperty_ReturnBadRequest_WithErrorMessage()
    {
        //Arrange
        //Act
        //Assert
    }
    [Fact]
    public async Task UpdateProperty_ReturnOk_WithResultTrue()
    {
        //Arrange
        //Act
        //Assert
    }
    [Fact]
    public async Task UpdateProperty_ReturnBadRequest_WithErrorMessage()
    {
        //Arrange
        //Act
        //Assert
    }
    [Fact]
    public async Task DeleteProperty_ReturnOk_WithResultTrue()
    {
        //Arrange
        //Act
        //Assert
    }
    [Fact]
    public async Task DeletePropert_ReturnBadRequest_WithErrorMessage()
    {
        //Arrange
        //Act
        //Assert
    }
}

