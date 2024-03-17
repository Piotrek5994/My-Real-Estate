using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers;

[Route("[controller]")]
[ApiController]
public class PropertyTypeController : ControllerBase
{
    private readonly IPropertyTypeService _propertyTypeService;

    public PropertyTypeController(IPropertyTypeService propertyTypeService)
    {
        _propertyTypeService = propertyTypeService;
    }
    [HttpGet]
    public async Task<IActionResult> GetPropertyType()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreatePropertyType()
    {
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdatePropertyType()
    {
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeletePropertyType()
    {
        return Ok();
    }
}
