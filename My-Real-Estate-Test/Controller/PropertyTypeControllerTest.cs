using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
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
}
