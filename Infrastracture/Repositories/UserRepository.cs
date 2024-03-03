using Core.Commend;
using Core.IRepositories;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
