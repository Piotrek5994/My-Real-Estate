using Core.Commend;
using Core.IRepositories;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastracture.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MongoDbContext _context;
        private readonly ILogger _log;

        public AuthRepository(MongoDbContext context, ILogger<AuthRepository> log)
        {
            _context = context;
            _log = log;
        }
        public async Task<string> CreateUser(CreateUser user)
        {
            try
            {
                user.Role = "User";
                var collection = _context.GetCollection<CreateUser>("User");
                await collection.InsertOneAsync(user);
                return user.Id;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error creating user in MongoDB");
                return "faile";
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
                _log.LogError(ex, "Error creating user in MongoDB");
                return "faile";
            }
        }
    }
}
