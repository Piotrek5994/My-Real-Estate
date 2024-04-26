using AutoMapper.Features;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using My_Real_Estate.Controllers;
using System.Text.Json;
public class FeaturesControllerTest
{
    private readonly Mock<IFeaturesService> _featuresServiceMock;
    private readonly FeaturesController _featuresController;

    public FeaturesControllerTest()
    {
        _featuresServiceMock = new Mock<IFeaturesService>();
        _featuresController = new FeaturesController(_featuresServiceMock.Object);
    }
    [Fact]
    public async Task CreateFeatures_ReturnOkResult()
    {
        //Arrange
        var features = new List<string>
        {
            "TV"
        };
        string propertyId = "some-property-id";
        var expectedFeatureId = new List<string>()
        {
            "some-features-id"

        };
        _featuresServiceMock.Setup(s => s.CreateFeaturesDto(features, propertyId))
                            .ReturnsAsync(expectedFeatureId);

        //Act
        var controller = new FeaturesController(_featuresServiceMock.Object);
        var result = await controller.CreateFeatures(features, propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("FeaturesId"));
        Assert.Equal(expectedFeatureId, dictionary["FeaturesId"]);
    }
    [Fact]
    public async Task CreateFeatures_ReturnBadRequest_WithMessage()
    {
        //Arrange
        var features = new List<string>
        {
            "TV"
        };
        string propertyId = "some-property-id";
        _featuresServiceMock.Setup(s => s.CreateFeaturesDto(features, propertyId))
                            .ReturnsAsync((List<string>)null);

        //Act
        var controller = new FeaturesController(_featuresServiceMock.Object);
        var result = await controller.CreateFeatures(features, propertyId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        Assert.Equal("Create property fail.", dictionary["Message"]);
    }
    [Fact]
    public async Task UpdateFeatures_ReturnOkResult()
    {
        //Arrange
        string updateFeature = "Tv2";
        int featureIndex = 0;
        string propertyId = "Some-property-Id";
        _featuresServiceMock.Setup(s => s.UpdateFeatures(updateFeature, featureIndex, propertyId))
                                         .ReturnsAsync(true);

        //Act
        var controller = new FeaturesController(_featuresServiceMock.Object);
        var result = await controller.UpdateFeatures(updateFeature, featureIndex, propertyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Result"));
        bool message;
        dictionary.TryGetValue("Result",out message);
        Assert.True(message);
        Assert.Equal(true, message);

    }
    [Fact]
    public async Task UpdateFeatures_ReturnBadRequest_WithMessage()
    {
        //Arrange
        string updateFeature = "Tv2";
        int featureIndex = 0;
        string propertyId = "Some-property-Id";
        _featuresServiceMock.Setup(s => s.UpdateFeatures(updateFeature, featureIndex, propertyId))
                                         .ReturnsAsync(false);
        
        //Act
        var controller = new FeaturesController(_featuresServiceMock.Object);
        var result = await controller.UpdateFeatures(updateFeature, featureIndex, propertyId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message",out message);
        Assert.Equal("Features update fail.", message);
    }
    [Fact]
    public async Task DeleteFeatures_ReturnOkResult()
    {
        //Arrange
        int featuresIndex = 0;
        string proprtyId = "some-property-Id";
        _featuresServiceMock.Setup(s => s.DeleteFeatures(featuresIndex, proprtyId))
                                         .ReturnsAsync(true);
        
        //Act
        var controller = new FeaturesController(_featuresServiceMock.Object);
        var result = await controller.DeleteFeatures(featuresIndex, proprtyId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var json = JsonSerializer.Serialize(okResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string,bool>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Result"));
        bool message;
        dictionary.TryGetValue("Result",out message);
        Assert.True(message);
        Assert.Equal(true, message);
    }
    [Fact]
    public async Task DeleteFeatures_ReturnBadRequest_WithMessage()
    {
        //Arrange
        int featuresIndex = 0;
        string proprtyId = "some-property-Id";
        _featuresServiceMock.Setup(s => s.DeleteFeatures(featuresIndex, proprtyId))
                                         .ReturnsAsync(false);

        //Act
        var controller = new FeaturesController(_featuresServiceMock.Object);
        var result = await controller.DeleteFeatures(featuresIndex, proprtyId);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        var json = JsonSerializer.Serialize(badRequestResult.Value);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        Assert.NotNull(dictionary);
        Assert.True(dictionary.ContainsKey("Message"));
        string message;
        dictionary.TryGetValue("Message", out message);
        Assert.Equal("Features does not exist.", message);
    }
}

