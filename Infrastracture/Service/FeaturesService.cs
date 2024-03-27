using AutoMapper;
using Core.Command.Update;
using Core.CommandDto;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.ModelDto;
using Infrastracture.Service.IService;

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
    public async Task<bool> UpdateFeatures(UpdateFeature updateFeature, string featuresId) => await _featuresRepository.UpdateFeature(updateFeature, featuresId);
}
