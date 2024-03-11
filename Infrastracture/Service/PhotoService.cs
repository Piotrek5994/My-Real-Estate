﻿using AutoMapper;
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
    private readonly IMapper _mapper;

    public PhotoService(IPhotoRepository photoRepository,IMapper mapper)
    {
        _photoRepository = photoRepository;
        _mapper = mapper;
    }
}
