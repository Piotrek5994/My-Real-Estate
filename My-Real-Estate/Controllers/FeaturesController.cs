using Core.CommandDto;
using Core.ModelDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers;

[ApiController]
[Route("[controller]")]
public class FeaturesController : Controller
{
    private readonly IFeaturesService _featuresService;

    public FeaturesController(IFeaturesService featuresService)
    {
        _featuresService = featuresService;
    }
    [HttpGet]
    public async Task<IActionResult> GetFeatures()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreateFeatures([FromBody] List<CreateFeaturesDto> featuresDto, string propertyId)
    {
        var featuresId = await _featuresService.CreateFeaturesDto(featuresDto, propertyId);

        if (featuresId == null)
        {
            return BadRequest(new { message = "Create property fail" });
        }

        return Ok(new { FeaturesId = featuresId });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateFeatures()
    {
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteFeatures()
    {
        return Ok();
    }
}
