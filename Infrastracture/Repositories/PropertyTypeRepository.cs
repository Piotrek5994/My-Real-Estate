using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastracture.Repositories;

public class PropertyTypeRepository : IPropertyTypeRepository
{
    private readonly MongoDbContext _context;
    private readonly IPropertyRepository _propertyRepository;
    private readonly ILogger _log;

    public PropertyTypeRepository(MongoDbContext context, ILogger<PropertyTypeRepository> log, IPropertyRepository propertyRepository)
    {
        _context = context;
        _log = log;
        _propertyRepository = propertyRepository;
    }
    public async Task<List<PropertyType>> GetPropertyType(PropertyTypeFilter filter)
    {
        try
        {

            var collection = _context.GetCollection<PropertyType>("PropertyType");
            var filterDefinitions = new List<FilterDefinition<PropertyType>>();
            filterDefinitions.Add(Builders<PropertyType>.Filter.Empty);

            if (!string.IsNullOrEmpty(filter.Id))
            {
                filterDefinitions.Add(Builders<PropertyType>.Filter.Eq("_id", ObjectId.Parse(filter.Id)));
            }
            if (!string.IsNullOrEmpty(filter.PropertyId))
            {
                filterDefinitions.Add(Builders<PropertyType>.Filter.Eq("property_id", ObjectId.Parse(filter.PropertyId)));
            }

            var combinedFilter = Builders<PropertyType>.Filter.And(filterDefinitions);

            // Define sorting
            var sortDefinition = filter.SortDescending ?
                                 Builders<PropertyType>.Sort.Descending(filter.SortBy) :
                                 Builders<PropertyType>.Sort.Ascending(filter.SortBy);

            // Pagination and apply sorting
            var propertType = await collection.Find(combinedFilter)
                                              .Sort(sortDefinition)
                                              .Skip((filter.Page - 1) * filter.Limit)
                                              .Limit(filter.Limit)
                                              .ToListAsync();

            return propertType;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error getting property(s) type : Message - {ex.Message}");
            return null;
        }
    }
    public async Task<string> CreatePropertyType(CreatePropertyType propertyType, string propertyId)
    {
        try
        {
            var chackProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (chackProperty == null || !chackProperty.Any())
            {
                _log.LogWarning("Warning: chosen property does not exist");
                return "The chosen property not exist";
            }
            propertyType.PropertyId = propertyId;

            var collection = _context.GetCollection<CreatePropertyType>("PropertyType");
            var chackPropeert = await collection.Find(p => p.PropertyId == propertyId).FirstOrDefaultAsync();
            if (chackPropeert != null)
            {
                _log.LogWarning("Warning: Property already has an property type");
                return "The property already has an propertyType";
            }

            await collection.InsertOneAsync(propertyType);
            return propertyType.Id;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create property type Message : {ex.Message}");
            return null;
        }
    }
    public async Task<bool> UpdatePropertyType(UpdatePropertyType propertyType, string propertyId)
    {
        try
        {
            var chackProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (chackProperty == null || !chackProperty.Any())
            {
                _log.LogWarning("Warning: Property is not exist");
                return false;
            }

            var collection = _context.GetCollection<PropertyType>("PropertyType");
            var findPropertyType = await collection.Find(p => p.PropertyId == propertyId).FirstOrDefaultAsync();

            var filter = Builders<PropertyType>.Filter.Eq("_id", ObjectId.Parse(findPropertyType.Id));
            var update = Builders<PropertyType>.Update.Set(p => p.PropertyTypeName, propertyType.propertyTypeName);

            await collection.UpdateOneAsync(filter, update);

            return true;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error update property type Message : {ex.Message}");
            return false;
        }
    }
}
