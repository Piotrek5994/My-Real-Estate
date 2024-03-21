using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
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
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error getting feature(s) type : Message - {ex.Message}");
            return null;
        }
    }
    public async Task<List<string>> CreateFeatures(CreateFeatures features, string propertyId)
    {
        var resultMessages = new List<string>();
        try
        {

            var chackProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (chackProperty == null || !chackProperty.Any())
            {
                _log.LogWarning("Warning: chosen property does not exist");
                resultMessages.Add("The chosen property not exist");
                return resultMessages;
            }
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            resultMessages.Add($"An error occurred while creating features: {ex.Message}");
            return resultMessages;
        }
    }
    public async Task<bool> UpdateFeature(UpdateFeature feature, string propertyId)
    {
        try
        {
            return true;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            return false;
        }
    }
}
