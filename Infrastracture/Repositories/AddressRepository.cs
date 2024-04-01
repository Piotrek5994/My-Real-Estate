using Core.IRepositories;
using Infrastracture.Mapper;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Commend.Create;
using Core.Filter;
using Core.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Core.Command.Update;
using Core.Commend.Update;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastracture.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger _log;
    private readonly IUserRepository _userRepository;
    private readonly IPropertyRepository _propertyRepository;

    public AddressRepository(MongoDbContext context, ILogger<AddressRepository> log, IUserRepository userRepository, IPropertyRepository propertyRepository)
    {
        _context = context;
        _log = log;
        _userRepository = userRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<List<Address>> GetAddress(AddressFilter filter)
    {
        try
        {
            var collection = _context.GetCollection<Address>("Address");
            Console.WriteLine($"Collection length: {collection.EstimatedDocumentCount}");
            var filterDefinitions = new List<FilterDefinition<Address>>
            {
                Builders<Address>.Filter.Empty
            };

            if (!string.IsNullOrEmpty(filter.Id))
            {
                filterDefinitions.Add(Builders<Address>.Filter.Eq("_id", ObjectId.Parse(filter.Id)));
            }
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                filterDefinitions.Add(Builders<Address>.Filter.Eq("user_id", ObjectId.Parse(filter.UserId)));
            }
            else if (!string.IsNullOrEmpty(filter.PropertyId))
            {
                filterDefinitions.Add(Builders<Address>.Filter.Eq("property_id", ObjectId.Parse(filter.PropertyId)));
            }
            var combinedFilter = Builders<Address>.Filter.And(filterDefinitions);
            
            var sortDefinition = filter.SortDescending ?
                Builders<Address>.Sort.Descending(filter.SortBy) :
                Builders<Address>.Sort.Ascending(filter.SortBy);
            
            var address = await collection.Find(combinedFilter)
                .Sort(sortDefinition)
                .Skip((filter.Page - 1) * filter.Limit)
                .Limit(filter.Limit)
                .ToListAsync();
            return address;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error getting address : Message - {ex.Message}");
            return null;
        }
    }

    public async Task<string> CreateAddress(CreateAddress address)
    {
        try
        {
            var collection = _context.GetCollection<CreateAddress>("Address");
            var checkProperty = address.PropertyId == null ? null : await _propertyRepository.GetProperty(new PropertyFilter { Id = address.PropertyId });
            var checkUser = address.UserId == null ? null : await _userRepository.GetUser(new UserFilter { Id = address.UserId });
            if (checkProperty == null && checkUser != null)
            {
                var filterUser = Builders<CreateAddress>.Filter.Eq("user_id", ObjectId.Parse(address.UserId));
                Console.WriteLine(collection.CountDocuments(filterUser));
                if (collection.CountDocuments(filterUser) != 0)
                {
                    _log.LogWarning("Warning: User already has an address, update already existing one instead of creating a new one!");
                    return "User already has an address, update already existing one instead of creating a new one!";
                }
                else if (checkUser.Count == 0)
                {
                    _log.LogWarning("Warning: Chosen user does not exist");
                    return "The chosen user not exist";
                }
            }
            else if (checkUser == null && checkProperty != null)
            {
                var filterProperty = Builders<CreateAddress>.Filter.Eq("property_id", ObjectId.Parse(address.PropertyId));
                if (collection.CountDocuments(filterProperty) != 0)
                {
                    _log.LogWarning("Warning: Property already has an address, update already existing one instead of creating a new one!");
                    return "Property already has an address, update already existing one instead of creating a new one!";
                }
                else if (checkProperty.Count == 0)
                {
                    _log.LogWarning("Warning: Chosen property does not exist");
                    return "The chosen property not exist";
                }

            }
            else if (checkUser == null && checkProperty == null)
            {
                _log.LogWarning("Warning: UserId and propertyId can't be null at the same time!");
                return "UserId and propertyId can't be null at the same time!";
            }
            await collection.InsertOneAsync(address);
            return address.Id;
        }
        catch (Exception ex) 
        {
            _log.LogError(ex, $"Error create address, Message : {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateAddress(UpdateAddress updateAddress, string addressId)
    {
        try
        {
            var collection = _context.GetCollection<Address>("Address");
            var filter = Builders<Address>.Filter.Eq("_id", ObjectId.Parse(addressId));
            var updates = new List<UpdateDefinition<Address>>();

            if (!string.IsNullOrEmpty(updateAddress.Country))
                updates.Add(Builders<Address>.Update.Set(u => u.Country, updateAddress.Country));
            if (!string.IsNullOrEmpty(updateAddress.Region))
                updates.Add(Builders<Address>.Update.Set(u => u.Region, updateAddress.Region));
            if (!string.IsNullOrEmpty(updateAddress.ZipCode))
                updates.Add(Builders<Address>.Update.Set(u => u.ZipCode, updateAddress.ZipCode));
            if (!string.IsNullOrEmpty(updateAddress.City))
                updates.Add(Builders<Address>.Update.Set(u => u.City, updateAddress.City));
            if (!string.IsNullOrEmpty(updateAddress.StreetName))
                updates.Add(Builders<Address>.Update.Set(u => u.StreetName, updateAddress.StreetName));
            if (!string.IsNullOrEmpty(updateAddress.StreetNumber))
                updates.Add(Builders<Address>.Update.Set(u => u.StreetNumber, updateAddress.StreetNumber));


            if (updates.Count == 0)
            {
                return false;
            }

            var combinedUpdate = Builders<Address>.Update.Combine(updates);
            var result = await collection.UpdateOneAsync(filter, combinedUpdate);

            return result.ModifiedCount > 0;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error updating address : Message - {ex.Message}");
            return false;
        }
        
    }
    public async Task<bool> DeleteAddress(string addressId)
    {
        try
        {
            var collection = _context.GetCollection<Address>("Address");
            var filter = Builders<Address>.Filter.Eq("_id", ObjectId.Parse(addressId));
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error deleting address : Message - {ex.Message}");
            return false;
        }
    }
}
