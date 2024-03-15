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
    [HttpGet]
    [Route("/Avatar")]
    [Produces("image/jpeg", "image/png")]
    public async Task<IActionResult> GetAvatarPhoto(string userId)
    {
        var result = await _photoService.GetAvatar(userId);
        return Ok(result);
    }

    [HttpPost]
    [Route("/Avatar")]
    public async Task<IActionResult> UploadAvatarPhoto(IFormFile formFile, string userId)
    {
        if (userId == null || string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { Message = "User ID is requaierd." });
        }

        var result = await _photoService.UploadPhoto(formFile, userId, "Avatar");
        if (result == null)
        {
            return BadRequest(new { Message = "Error loading image." });
        }

        return Ok(new { AvatarId = result });
    }
    [HttpPut]
    [Route("/Avatar")]
    public async Task<IActionResult> UpdateAvatarPhoto(IFormFile formFile,string userId)
    {
        var result = await _photoService.ChangePhoto(formFile,userId, "ChangeAvatar");
        return Ok(new { AvatarId = result });
    }
    [HttpDelete]
    [Route("/Avatar")]
    public async Task<IActionResult> DeleteAvatarPhoto(string userId)
    {
         await _photoService.DeleteAvatar(userId);
        return Ok(new {message = true});
    }
}
