using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController : Controller
{
    private readonly IPhotoService _photoService;

    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }
    [HttpPost]
    [Route("/Avatar")]
    public async Task<IActionResult> UploadAvatarPhoto(IFormFile formFile, string userId)
    {
        if (userId == null || string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { Message = "Invalid user ID." });
        }

        var result = await _photoService.UploadPhoto(formFile, userId,"Avatar");
        if (result == null)
        {
            return BadRequest(new { Message = "Error loading image." });
        }

        return Ok(new { AvatarId = result });
    }
}
