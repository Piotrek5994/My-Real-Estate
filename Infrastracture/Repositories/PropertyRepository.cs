using Core.Commend.Create;
using Core.Commend.Update;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
    public async Task<Property> GetProperty(PropertyFilter filter)
    {
        return null;
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
    public async Task DeleteProperty()
    {

    }
}
