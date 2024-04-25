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

        //Act

        //Assert
    }
    [Fact]
    public async Task UpdateFeatures_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task UpdateFeatures_ReturnBadRequest_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task DeleteFeatures_ReturnOkResult()
    {
        //Arrange

        //Act

        //Assert
    }
    [Fact]
    public async Task DeleteFeatures_ReturnBadRequest_WithMessage()
    {
        //Arrange

        //Act

        //Assert
    }
}

