using Infrastracture.Service.IService;
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
        var streamResult = await _photoService.GetAvatar(userId);
        if (streamResult == null)
        {
            return NotFound(new { Message = "The user does not exist or does not have an avatar." });
        }
        using (var memoryStream = new MemoryStream())
        {
            await streamResult.CopyToAsync(memoryStream);
            byte[] photoBytes = memoryStream.ToArray();
            string contentType = "image/jpeg";
            if (photoBytes.Length > 0 && photoBytes[0] == 0x89 && photoBytes[1] == 0x50)
            {
                contentType = "image/png";
            }
            return new FileContentResult(photoBytes, contentType);
        }
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
        return Ok(new { ChangeAvatar = result });
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
    [Produces("image/jpeg", "image/png")]
    public async Task<IActionResult> GetPropertyPhoto(string propertyId, string photoId)
    {
        var streamPhoto = await _photoService.GetPropertyPhotos(propertyId, photoId);
        if (streamPhoto == null)
        {
            return NotFound(new { Message = "The property does not exist or does not have an photo." });
        }
        using (var memoryStream = new MemoryStream())
        {
            await streamPhoto.CopyToAsync(memoryStream);
            byte[] photoBytes = memoryStream.ToArray();
            string contentType = "image/jpeg";
            if (photoBytes.Length > 0 && photoBytes[0] == 0x89 && photoBytes[1] == 0x50)
            {
                contentType = "image/png";
            }
            return new FileContentResult(photoBytes, contentType);
        }
    }
    [HttpPost]
    [Route("/Photo")]
    public async Task<IActionResult> UploadPropertyPhotos(IFormFileCollection formFilesCollection, string propertyId)
    {
        var photoIdList = await _photoService.UploadsPropertyPhotos(formFilesCollection, propertyId);
        if (photoIdList == null || !photoIdList.Any())
        {
            return BadRequest(new { Message = "Create photo for property fail" });
        }
        return Ok(new { PhotoId = photoIdList });
    }
    [HttpPut]
    [Route("/Photo")]
    public async Task<IActionResult> UpdatePropertyPhotos(IFormFile formFile, string photoId, string propertyId)
    {
        var result = await _photoService.ChangePropertyPhoto(formFile, photoId, propertyId);
        return Ok(new { ChangePropertyPhoto = result });
    }
    [HttpDelete]
    [Route("/Photo")]
    public async Task<IActionResult> DeletePropertyPhotos([FromBody] List<string> photoId, string propertyId)
    {
        bool result = await _photoService.DeletePropertyPhotos(photoId, propertyId);
        return Ok(new { Success = result });
    }
}
