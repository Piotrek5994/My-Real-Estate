using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.Model;

namespace Core.IRepositories;

public interface IFeaturesRepository
{
    Task<List<string>> CreateFeatures(List<string> features, string propertyId);
    Task<bool> UpdateFeature(string updatefeature, int featuresId, string propertyId);
    Task<bool> DeleteFeature(int featureId, string propertyId);
}
