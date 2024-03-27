using Core.Command.Update;
using Core.Commend.Create;
using Core.Commend.Update;
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
    public async Task<List<Features>> GetFeatures(FeaturesFilter filter)
    {
        try
        {
            var collection = _context.GetCollection<Features>("Features");
            var filterDefinitions = new List<FilterDefinition<Features>>();
            filterDefinitions.Add(Builders<Features>.Filter.Empty);

            if (!string.IsNullOrEmpty(filter.Id))
            {
                filterDefinitions.Add(Builders<Features>.Filter.Eq("_id", ObjectId.Parse(filter.Id)));
            }
            if (!string.IsNullOrEmpty(filter.PropertyId))
            {
                filterDefinitions.Add(Builders<Features>.Filter.Eq("property_id", ObjectId.Parse(filter.PropertyId)));
            }

            var combinedFilter = Builders<Features>.Filter.And(filterDefinitions);

            var sortDefinition = filter.SortDescending ?
                                 Builders<Features>.Sort.Descending(filter.SortBy) :
                                 Builders<Features>.Sort.Ascending(filter.SortBy);

            var properties = await collection.Find(combinedFilter)
                                             .Sort(sortDefinition)
                                             .Skip((filter.Page - 1) * filter.Limit)
                                             .Limit(filter.Limit)
                                             .ToListAsync();

            return properties;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error getting feature(s) type : Message - {ex.Message}");
            return null;
        }
    }
    public async Task<List<string>> CreateFeatures(List<CreateFeatures> features, string propertyId)
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

            foreach (var feature in features)
            {
                feature.Id = ObjectId.GenerateNewId().ToString();
                feature.PropertyId = propertyId;
            }

            var collection = _context.GetCollection<CreateFeatures>("Features");

            await collection.InsertManyAsync(features);


            var createdFeatures = features.Select(feature => feature.Id).ToList();

            var collectionUser = _context.GetCollection<Property>("Property");
            var filter = Builders<Property>.Filter.Eq("_id", ObjectId.Parse(propertyId));
            var update = Builders<Property>.Update.AddToSetEach(p => p.Features, createdFeatures);
            await collectionUser.UpdateOneAsync(filter, update);

            return createdFeatures;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            resultMessages.Add($"An error occurred while creating features: {ex.Message}");
            return resultMessages;
        }
    }
    public async Task<bool> UpdateFeature(UpdateFeature updatefeature, string featuresId)
    {
        try
        {
            var collection = _context.GetCollection<Features>("Features");
            var filter = Builders<Features>.Filter.Eq("_id", ObjectId.Parse(featuresId));

            if (!string.IsNullOrEmpty(updatefeature.FeatureName))
            {
                var updates = Builders<Features>.Update.Set(u => u.FeatureName, updatefeature.FeatureName);
                var result = await collection.UpdateOneAsync(filter, updates);
                return result.ModifiedCount > 0;
            }
            _log.LogError($"Error update feature(s) type feature name is null or empty");
            return false;

        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteFeature(string featureId)
    {
        try
        {
            var collection = _context.GetCollection<Features>("Features");
            var featureFilter = Builders<Features>.Filter.Eq("_id", ObjectId.Parse(featureId));
            var feature = await collection.Find(featureFilter).FirstOrDefaultAsync();

            if (feature == null)
            {
                _log.LogWarning($"Feature with ID {featureId} not found.");
                return false;
            }

            string propertyId = feature.PropertyId;
            var result = await collection.DeleteOneAsync(featureFilter);

            if (result.DeletedCount > 0)
            {
                var propertyCollection = _context.GetCollection<Property>("Property");
                var propertyFilter = Builders<Property>.Filter.Eq("_id", ObjectId.Parse(propertyId));
                var update = Builders<Property>.Update.Pull(p => p.Features, featureId);
                var updateResult = await propertyCollection.UpdateOneAsync(propertyFilter, update);
            }

            return result.DeletedCount > 0;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error deleting feature with ID {featureId}: {ex.Message}");
            return false;
        }
    }

}
