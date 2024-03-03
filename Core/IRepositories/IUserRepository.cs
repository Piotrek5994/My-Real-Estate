using Core.Commend;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUser(string userId);
        Task<string> CreateUser(CreateUser user);
        Task<string> CreateAdmin(CreateUser user);
    }
}
