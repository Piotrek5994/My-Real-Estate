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

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _log;

    public PropertyRepository(MongoDbContext context, ILogger<PropertyRepository> log, IUserRepository userRepository)
    {
        _context = context;
        _log = log;
        _userRepository = userRepository;
    }
    public async Task<List<Property>> GetProperty(PropertyFilter filter)
    {
        try
        {
            var collection = _context.GetCollection<Property>("Property");
            var filterDefinitions = new List<FilterDefinition<Property>>();
            filterDefinitions.Add(Builders<Property>.Filter.Empty);

            if (!string.IsNullOrEmpty(filter.Id))
            {
                filterDefinitions.Add(Builders<Property>.Filter.Eq("_id", ObjectId.Parse(filter.Id)));
            }
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                filterDefinitions.Add(Builders<Property>.Filter.Eq("user_id", ObjectId.Parse(filter.UserId)));
            }

            var combinedFilter = Builders<Property>.Filter.And(filterDefinitions);

            var sortDefinition = filter.SortDescending ?
                                 Builders<Property>.Sort.Descending(filter.SortBy) :
                                 Builders<Property>.Sort.Ascending(filter.SortBy);

            var properties = await collection.Find(combinedFilter)
                                             .Sort(sortDefinition)
                                             .Skip((filter.Page - 1) * filter.Limit)
                                             .Limit(filter.Limit)
                                             .ToListAsync();

            return properties;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, "Error getting property(s)");
            return null;
        }
    }
    public async Task<string> CreateProperty(CreateProperty property)
    {
        try
        {

            var collection = _context.GetCollection<CreateProperty>("Property");
            var chackUser = await _userRepository.GetUser(new UserFilter { Id = property.UserId });
            if (chackUser == null)
            {
                _log.LogError("Error: chosen user does not exist");
                return "The chosen user not exist";
            }
            await collection.InsertOneAsync(property);

            var collectionUser = _context.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(property.UserId));
            var update = Builders<User>.Update.AddToSet(a => a.Properties, property.Id);

            await collectionUser.UpdateOneAsync(filter, update);

            return property.Id;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create property Message : {ex.Message}");
            return null;
        }
    }
    public async Task<UpdateProperty> UpdateProperty()
    {
        return null;
    }
    public async Task<bool> DeleteProperty(string propertyId)
    {
        try
        {
            var collection = _context.GetCollection<Property>("Property");
            var filter = Builders<Property>.Filter.Eq("_id", ObjectId.Parse(propertyId));
            var result = await collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, "Error delete property in MongoDb.");
            return false;
        }
    }
}
