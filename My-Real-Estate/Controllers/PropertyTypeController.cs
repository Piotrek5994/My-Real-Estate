using Core.CommendDto;
using Core.Filter;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers;

[Route("[controller]")]
[ApiController]
public class PropertyTypeController : Controller
{
    private readonly IPropertyTypeService _propertyTypeService;

    public PropertyTypeController(IPropertyTypeService propertyTypeService)
    {
        _propertyTypeService = propertyTypeService;
    }
    [HttpGet]
    public async Task<IActionResult> GetPropertyType([FromQuery]PropertyTypeFilter filter)
    {
        var propertyType = await _propertyTypeService.GetPropertyTypeDto(filter);
        if (propertyType != null)
        {
            return Json(new { result = propertyType });
        }
        return NotFound(new { message = "PropertyType don't found" });
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
