using Core.Commend.Create;
using Core.Commend.Update;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastracture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;
        private readonly ILogger _log;

        public UserRepository(MongoDbContext context, ILogger<UserRepository> log)
        {
            _context = context;
            _log = log;
        }
        public async Task<List<User>> GetUser(UserFilter filter)
        {
            try
            {
                var collection = _context.GetCollection<User>("User");
                var filterDefinition = Builders<User>.Filter.Empty;

                if (!string.IsNullOrEmpty(filter.Id))
                {
                    filterDefinition = Builders<User>.Filter.Where(user => user.Id.Contains(filter.Id));
                }
                // Define sorting
                var sortDefinition = filter.SortDescending ?
                                     Builders<User>.Sort.Descending(filter.SortBy) :
                                     Builders<User>.Sort.Ascending(filter.SortBy);

                // Pagination and apply sorting
                var users = collection.Find(filterDefinition)
                                      .Sort(sortDefinition)
                                      .Skip((filter.Page - 1) * filter.Limit)
                                      .Limit(filter.Limit);

                return await users.ToListAsync();
            }
            catch (MongoException ex)
            {
                _log.LogError(ex, "Error getting user(s)");
                return null;
            }
        }
        public async Task<string> CreateUser(CreateUser user)
        {
            try
            {
                var collection = _context.GetCollection<CreateUser>("User");
                var existingUser = await collection.Find(u => u.Email == user.Email).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    return "The user with the provided email address is already registered.";
                }
                user.Role = "User";
                await collection.InsertOneAsync(user);
                return user.Id;
            }
            catch (MongoException ex)
            {
                _log.LogError(ex, "Error creating user in MongoDB.");
                return "failed";
            }
        }
        public async Task<string> CreateAdmin(CreateUser user)
        {
            try
            {
                user.Role = "Admin";
                var collection = _context.GetCollection<CreateUser>("User");
                await collection.InsertOneAsync(user);
                return user.Id;
            }
            catch (MongoException ex)
            {
                _log.LogError(ex, "Error creating user in MongoDB.");
                return "faile";
            }
        }
        public async Task<bool> UpdateUser(UpdateUser updateUser, string userId)
        {
            try
            {
                var collection = _context.GetCollection<User>("User");
                var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(userId));

                var updates = new List<UpdateDefinition<User>>();

                if (!string.IsNullOrEmpty(updateUser.FirstName))
                    updates.Add(Builders<User>.Update.Set(u => u.FirstName, updateUser.FirstName));
                if (!string.IsNullOrEmpty(updateUser.LastName))
                    updates.Add(Builders<User>.Update.Set(u => u.LastName, updateUser.LastName));
                if (!string.IsNullOrEmpty(updateUser.Gender))
                    updates.Add(Builders<User>.Update.Set(u => u.Gender, updateUser.Gender));
                if (!string.IsNullOrEmpty(updateUser.PESEL))
                    updates.Add(Builders<User>.Update.Set(u => u.PESEL, updateUser.PESEL));
                if (!string.IsNullOrEmpty(updateUser.Email))
                    updates.Add(Builders<User>.Update.Set(u => u.Email, updateUser.Email));
                if (!string.IsNullOrEmpty(updateUser.Password))
                    updates.Add(Builders<User>.Update.Set(u => u.Password, updateUser.Password));
                if (!string.IsNullOrEmpty(updateUser.PhoneNumber))
                    updates.Add(Builders<User>.Update.Set(u => u.PhoneNumber, updateUser.PhoneNumber));

                if (updates.Count == 0)
                {
                    return false;
                }

                var combinedUpdate = Builders<User>.Update.Combine(updates);
                var result = await collection.UpdateOneAsync(filter, combinedUpdate);

                return result.ModifiedCount > 0;
            }
            catch (MongoException ex)
            {
                _log.LogError(ex, "Error updating user in MongoDB.");
                return false;
            }
        }


        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                var collection = _context.GetCollection<User>("User");
                var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.DeleteOneAsync(filter);

                return result.DeletedCount > 0;
            }
            catch (MongoException ex)
            {
                _log.LogError(ex, "Error delete user in MongoDb.");
                return false;
            }
        }
    }
}
