using Core.Commend;
using Core.Filter;
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
        Task<List<User>> GetUser(UserFilter filter);
        Task<string> CreateUser(CreateUser user);
        Task<string> CreateAdmin(CreateUser user);
        Task<bool> DeleteUser(string id);
    }
}
