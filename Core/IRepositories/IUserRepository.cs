using Core.Commend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IUserRepository
    {
        Task<string> CreateUser(CreateUser user);
        Task<string> CreateAdmin(CreateUser user);
    }
}
