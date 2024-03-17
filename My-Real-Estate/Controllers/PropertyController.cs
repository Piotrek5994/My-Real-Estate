using Core.Commend.Create;
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
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto propertyDto)
    {
        return Ok();
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
