using Core.Commend;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

                // Pagination
                var users = collection.Find(filterDefinition)
                                            .Skip((filter.Page - 1) * filter.Limit)
                                            .Limit(filter.Limit);

                return await users.ToListAsync();
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                _log.LogError(ex, "Error creating user in MongoDB.");
                return "faile";
            }
        }
    }
}
