using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.Model;

namespace Core.IRepositories;

public interface IFeaturesRepository
{
    Task<List<Features>> GetFeatures(FeaturesFilter filter);
    Task<List<string>> CreateFeatures(List<CreateFeatures> features, string propertyId);
    Task<bool> UpdateFeature(UpdateFeature updatefeature, string featuresId);
    Task<bool> DeleteFeature(string featureId);
}
