using Core.IRepositories;
using Infrastracture.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;

    public PhotoService(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }
}
