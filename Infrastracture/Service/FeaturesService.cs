using AutoMapper;
using Core.CommandDto;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Core.ModelDto;
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
    public async Task<List<FeaturesDto>> GetFeaturesDto(FeaturesFilter filter)
    {
        var result = await _featuresRepository.GetFeatures(filter);
        var propertyDto = _mapper.Map<List<FeaturesDto>>(result);
        return propertyDto;
    }
    public async Task<List<string>> CreateFeaturesDto(List<CreateFeaturesDto> featuresDto, string propertyId)
    {
        List<CreateFeatures> features = _mapper.Map<List<CreateFeatures>>(featuresDto);
        var result = await _featuresRepository.CreateFeatures(features, propertyId);
        return result;
    }
}
