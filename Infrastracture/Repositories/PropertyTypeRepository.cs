using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
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
        return null;
    }
    public async Task<string> CreatePropertyType(CreatePropertyType propertyType, string propertyId)
    {
        try
        {
            var chackProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (chackProperty == null)
            {
                _log.LogError("Error: chosen property does not exist");
                return "The chosen property not exist";
            }
            propertyType.PropertyId = propertyId;

            var collection = _context.GetCollection<CreatePropertyType>("PropertyType");
            var chackPropeert = await collection.Find(p => p.PropertyId == propertyId).FirstOrDefaultAsync();
            if(chackPropeert != null)
            {
                _log.LogError("Error: Property already has an property type");
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
    public async Task<bool> UpdatePropertyType(string propertyTypeName, string propertyId)
    {
        return false;
    }
    public async Task<bool> DeletePropertyType(string propertyId)
    {
        return false;
    }
}
