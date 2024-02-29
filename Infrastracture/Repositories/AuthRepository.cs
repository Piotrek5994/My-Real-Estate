using Core.IRepositories;
using Infrastructure.Db;
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

        public AuthRepository(MongoDbContext context)
        {
            _context = context;
        }
    }
}
