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
    [HttpPost]
    public async Task<IActionResult> CreateFeatures([FromBody] List<string> featuresDto, string propertyId)
    {
        var featuresId = await _featuresService.CreateFeaturesDto(featuresDto, propertyId);

        if (featuresId == null)
        {
            return BadRequest(new { Message = "Create property fail" });
        }

        return Ok(new { FeaturesId = featuresId });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateFeatures([FromQuery] string updateFeature, int featuresId, string propertyId)
    {
        bool update = await _featuresService.UpdateFeatures(updateFeature, featuresId, propertyId);
        if (!update)
        {
            return BadRequest(new { Message = "Features update fail" });
        }
        return Ok(new { Result = update });
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteFeatures([FromQuery] int featuresId, string propertyId)
    {
        bool delete = await _featuresService.DeleteFeatures(featuresId, propertyId);
        if (!delete)
        {
            return BadRequest(new { Message = "Features does not exist." });
        }
        return Ok(new { Result = delete });
    }
}
