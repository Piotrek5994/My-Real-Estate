using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
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

    public PropertyControllerTest(Mock<IPropertyService> propertyServiceMock, PropertyController propertyController)
    {
        _propertyServiceMock = new Mock<IPropertyService>();
        _propertyController = new PropertyController(_propertyServiceMock.Object);
    }
}

