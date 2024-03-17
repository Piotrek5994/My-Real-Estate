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
    public async Task<IActionResult> CreateFeatures()
    {
        return Ok();
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
