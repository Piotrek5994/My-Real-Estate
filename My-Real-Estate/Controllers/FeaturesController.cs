using Core.Command.Update;
using Core.CommandDto;
using Core.Filter;
using Infrastracture.Service;
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
    public async Task<IActionResult> GetFeatures([FromQuery] FeaturesFilter filter)
    {
        var features = await _featuresService.GetFeaturesDto(filter);
        if (features == null)
        {
            return NotFound(new { Message = "Feature of Features don't found" });
        }
        return Json(new { Result = features });
    }
    [HttpPost]
    public async Task<IActionResult> CreateFeatures([FromBody] List<CreateFeaturesDto> featuresDto, string propertyId)
    {
        var featuresId = await _featuresService.CreateFeaturesDto(featuresDto, propertyId);

        if (featuresId == null)
        {
            return BadRequest(new { Message = "Create property fail" });
        }

        return Ok(new { FeaturesId = featuresId });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateFeatures([FromBody]UpdateFeature updateFeature, string featuresId)
    {
        bool update = await _featuresService.UpdateFeatures(updateFeature, featuresId);
        if (!update)
        {
            return BadRequest(new { Message = "Features update fail" });
        }
        return Ok(new { Result = update });
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteFeatures()
    {
        return Ok();
    }
}
