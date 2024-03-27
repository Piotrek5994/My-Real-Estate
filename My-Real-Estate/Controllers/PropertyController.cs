using Core.Commend.Update;
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
            return Json(new { Result = property });
        }
        return NotFound(new { Message = "Property of Propertys don't found" });
    }
    [HttpPost]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto propertyDto)
    {
        var propertyId = await _propertyService.CreatePropertyDto(propertyDto);
        if (propertyId == null)
        {
            return BadRequest(new { Message = "Create property fail" });
        }
        return Ok(new { PropertyId = propertyId });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty property, string properyId)
    {
        bool update = await _propertyService.UpdateProperty(property, properyId);
        if (!update)
        {
            return BadRequest(new { Message = "Property update fail" });
        }
        return Ok(new { Result = update });
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProperty(string propertyId)
    {
        bool delete = await _propertyService.DeleteProperty(propertyId);
        if (!delete)
        {
            return BadRequest(new { Message = "Property does not exist." });
        }
        return Ok(new { Result = delete });
    }
}
