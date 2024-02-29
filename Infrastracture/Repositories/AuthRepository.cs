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
    public class AuthRepository : IAuthRepository
    {
        private readonly MongoDbContext _context;
        private readonly ILogger _log;

        public AuthRepository(MongoDbContext context, ILogger<AuthRepository> log)
        {
            _context = context;
            _log = log;
        }
    }
}
