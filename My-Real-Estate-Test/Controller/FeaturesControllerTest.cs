using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;
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
}

