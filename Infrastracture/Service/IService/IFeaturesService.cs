namespace Infrastracture.Service.IService
{
    public interface IFeaturesService
    {
        Task<List<string>> CreateFeaturesDto(List<string> featuresDto, string propertyId);
        Task<bool> UpdateFeatures(string updatefeature, int featuresId, string propertyId);
        Task<bool> DeleteFeatures(int featureId, string propertyId);
    }
}
