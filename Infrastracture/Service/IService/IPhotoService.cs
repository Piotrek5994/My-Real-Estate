using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService;

public interface IPhotoService
{
    Task<Stream> GetAvatar(string userId);
    Task<string> UploadPhoto(IFormFile formFile, string userId, string operationName);
    Task<string> ChangePhoto(IFormFile formFile, string userId, string operationName);
    Task DeleteAvatar(string userId);
}
