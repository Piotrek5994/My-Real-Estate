using Core.Model;
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
    [Produces("image/jpeg", "image/png")]
    public async Task<IActionResult> GetPropertyPhoto(string propertyId, string photoId)
    {
        var photo = await _photoService.GetPropertyPhotos(propertyId,photoId);
        return Ok(photo);
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
    public async Task<IActionResult> UpdatePropertyPhotos(IFormFileCollection formFilesCollection, string photoId, string propertyId)
    {

        return Ok(new { Success = true });
    }
    [HttpDelete]
    [Route("/Photo")]
    public async Task<IActionResult> DeletePropertyPhotos([FromBody]List<string> photoId, string propertyId)
    {
        bool result = await _photoService.DeletePropertyPhotos(photoId,propertyId);
        return Ok(new { Success = result });
    }
}
