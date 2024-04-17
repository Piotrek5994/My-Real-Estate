using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IPhotoRepository
{
    Task<Stream> GetAvatarPhoto(string userId);
    Task<string> UploudAvatarPhoto(IFormFile formFile, string userId);
    Task<string> ChangeAvatarPhoto(IFormFile formFile, string userId);
    Task DeleteAvatarPhoto(string userId);
    Task DeleteAvatarPhotoHead(string userId);
    Task<Stream> GetPropertyPhoto(string propertyId, string photoId);
    Task<List<string>> UploudsPropertyPhotos(IFormFileCollection formFilesCollection, string propertyId);
    Task<List<string>> ChangesPropertyPhotos(IFormFile formFile, string photoId, string propertyId);
    Task<bool> DeletePropertyPhotos(List<string> photoIds, string propertyId);
    Task DeletePropertyPhotoHead(List<string> photoIds, string propertyId);
}
