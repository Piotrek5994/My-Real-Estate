using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastracture.Repositories;

public class FeaturesRepository : IFeaturesRepository
{
    private readonly MongoDbContext _context;
    private readonly IPropertyRepository _propertyRepository;
    private readonly ILogger _log;

    public FeaturesRepository(MongoDbContext context, ILogger<FeaturesRepository> log, IPropertyRepository propertyRepository)
    {
        _context = context;
        _log = log;
        _propertyRepository = propertyRepository;
    }
    public async Task<List<string>> CreateFeatures(List<string> features, string propertyId)
    {
        var resultMessages = new List<string>();
        try
        {

            var checkProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (checkProperty == null || !checkProperty.Any())
            {
                _log.LogWarning("Warning: chosen property does not exist");
                resultMessages.Add("The chosen property not exist");
                return resultMessages;
            }

            var collectionUser = _context.GetCollection<Property>("Property");
            var filter = Builders<Property>.Filter.Eq("_id", ObjectId.Parse(propertyId));
            var update = Builders<Property>.Update.AddToSetEach(p => p.Features, features);
            await collectionUser.UpdateOneAsync(filter, update);

            resultMessages.Add("Features added successfully.");
            return resultMessages;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            resultMessages.Add($"An error occurred while creating features: {ex.Message}");
            return resultMessages;
        }
    }
    public async Task<bool> UpdateFeature(string updatefeature, int featuresId, string propertyId)
    {
        try
        {
            var collection = _context.GetCollection<Property>("Property");
            var filter = Builders<Property>.Filter.Eq("_id", ObjectId.Parse(propertyId));

            var update = Builders<Property>.Update.Set($"Features.{featuresId}", updatefeature);
            var result = await collection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                return true;
            }
            else
            {
                _log.LogWarning("No update made, check the propertyId and feature index.");
                return false;
            }

        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error update feature type : Message - {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteFeature(int featureId, string propertyId)
    {
        try
        {
            var propertyCollection = _context.GetCollection<Property>("Property");
            var propertyFilter = Builders<Property>.Filter.Eq("_id", ObjectId.Parse(propertyId));
            var property = await propertyCollection.Find(propertyFilter).FirstOrDefaultAsync();

            if (property != null && property.Features.Count > featureId)
            {
                property.Features.RemoveAt(featureId);
                var replaceResult = await propertyCollection.ReplaceOneAsync(propertyFilter, property);
                if (replaceResult.ModifiedCount > 0)
                {
                    return true;
                }
                else
                {
                    _log.LogWarning("No update made, check the propertyId and index.");
                    return false;
                }
            }
            else
            {
                _log.LogWarning($"No property found or index {featureId} out of range.");
                return false;
            }
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error deleting feature with ID {featureId}: {ex.Message}");
            return false;
        }
    }
}
