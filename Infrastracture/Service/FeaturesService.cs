using AutoMapper;
using Core.IRepositories;
using Infrastracture.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service;

public class FeaturesService : IFeaturesService
{
    private readonly IFeaturesRepository _featuresRepository;
    private readonly IMapper _mapper;

    public FeaturesService(IFeaturesRepository featuresRepository, IMapper mapper)
    {
        _featuresRepository = featuresRepository;
        _mapper = mapper;
    }
}
