using Core.Command.Update;
using Core.CommandDto;
using Core.Filter;
using Core.ModelDto;

namespace Infrastracture.Service.IService
{
    public interface IFeaturesService
    {
        Task<List<FeaturesDto>> GetFeaturesDto(FeaturesFilter filter);
        Task<List<string>> CreateFeaturesDto(List<CreateFeaturesDto> featuresDto, string propertyId);
        Task<bool> UpdateFeatures(UpdateFeature updateFeature, string featuresId);
    }
}
