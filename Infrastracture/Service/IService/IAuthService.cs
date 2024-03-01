using Core.Commend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(CreateUser user,string role);
        Task<string> Login(CreateLogin login);
    }
}
