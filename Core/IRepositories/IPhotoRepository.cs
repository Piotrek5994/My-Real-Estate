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
}
