using Core.Command.Update;
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
            return BadRequest(new { Message = "User ID is required." });
        }

        string result = await _photoService.UploadPhoto(formFile, userId, "Avatar");
        if (result == null)
        {
            return BadRequest(new { Message = "Error loading image." });
        }

        return Ok(new { AvatarId = result });
    }
    [HttpPut]
    [Route("/Avatar")]
    public async Task<IActionResult> UpdateAvatarPhoto(IFormFile formFile, string userId)
    {
        string result = await _photoService.ChangePhoto(formFile, userId, "ChangeAvatar");
        return Ok(new { AvatarId = result });
    }
    [HttpDelete]
    [Route("/Avatar")]
    public async Task<IActionResult> DeleteAvatarPhoto(string userId)
    {
        await _photoService.DeleteAvatar(userId);
        return Ok(new { Success = true });
    }
    [HttpGet]
    [Route("/Photo")]
    public async Task<IActionResult> GetPropertyPhotos()
    {
        return Ok(new { Success = true });
    }
    [HttpPost]
    [Route("/Photo")]
    public async Task<IActionResult> UploadPropertyPhotos()
    {
        return Ok(new { Success = true });
    }
    [HttpPut]
    [Route("/Photo")]
    public async Task<IActionResult> UpdatePropertyPhotos(IFormFileCollection formFilesCollection, string photoId, string propertyId )
    {
        // Tutaj logika przetwarzania przesłanych danych
        // Przykład:
        // foreach (var photoUpdate in propertyPhotos)
        // {
        //     // aktualizuj zdjęcia dla photoUpdate.propertyId, itd.
        // }

        return Ok(new { Success = true });
    }
    [HttpDelete]
    [Route("/Photo")]
    public async Task<IActionResult> DeletePropertyPhotos()
    {
        return Ok(new { Success = true });
    }
}
