using AutoMapper;
using Core.IRepositories;
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
    public async Task<List<string>> CreateFeaturesDto(List<string> featuresDto, string propertyId)
    {
        var result = await _featuresRepository.CreateFeatures(featuresDto, propertyId);
        return result;
    }
    public async Task<bool> UpdateFeatures(string updatefeature, int featuresId, string propertyId) => await _featuresRepository.UpdateFeature(updatefeature, featuresId, propertyId);

    public async Task<bool> DeleteFeatures(int featureId, string propertyId) => await _featuresRepository.DeleteFeature(featureId, propertyId);
}
