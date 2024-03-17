using Core.CommendDto;
using Core.Filter;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProperty([FromQuery] PropertyFilter filter)
    {
        var property = await _propertyService.GetPropertyDto(filter);
        if (property != null)
        {
            return Json(new { result = property });
        }
        return NotFound(new {message = "Property of Propertys don't found"});
    }
    [HttpPost]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto propertyDto)
    {
        var propertyId = await _propertyService.CreatePropertyDto(propertyDto);
        if (propertyId == null)
        {
            return BadRequest(new { message = "Create property fail" });
        }
        return Ok(new { PropertyId = propertyId });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProperty()
    {
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProperty()
    {
        return Ok();
    }
}
