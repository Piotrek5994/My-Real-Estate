using Core.Commend;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

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
        public async Task<List<User>> GetUser(string userId)
        {
            try
            {
                var collection = _context.GetCollection<User>("User");

                if (string.IsNullOrEmpty(userId))
                {
                    return await collection.Find(_ => true).ToListAsync();
                }
                else
                { 
                    return await collection.Find(user => user.Id == userId).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"Error getting user(s). UserID: {userId}.");
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
