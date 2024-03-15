using AutoMapper;
using Core.IRepositories;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;

    public PhotoService(IPhotoRepository photoRepository,IMapper mapper)
    {
        _photoRepository = photoRepository;
        _mapper = mapper;
    }
    public async Task<Stream> GetAvatar(string userId)
    {
        Stream result = await _photoRepository.GetAvatarPhoto(userId);
        return result;
    }
    public async Task<string> UploadPhoto(IFormFile formFile,string userId,string operationName)
    {
        string result = operationName switch
        {
            string r when r.Contains("Avatar") => await _photoRepository.UploudAvatarPhoto(formFile, userId),
            _ => "Invalid operation"
        };

        return result;
    }
    public async Task DeleteAvatar(string userId)
    {
        await _photoRepository.DeleteAvatarPhoto(userId);
    }
}
