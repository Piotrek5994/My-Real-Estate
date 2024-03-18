using Core.CommendDto;
using Infrastracture.Service.IService;
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
    public async Task<IActionResult> CreatePropertyType([FromBody] CreatePropertyTypeDto propertyTypeDto, string propertyId)
    {
        var propertyTypeId = await _propertyTypeService.CreatePropertyTypeDto(propertyTypeDto, propertyId);
        if (propertyTypeId == null)
        {
            return BadRequest(new { message = "Create property type fail" });
        }
        return Ok(new { PropertyTypeId = propertyTypeId });
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
