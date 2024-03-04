using Core.Commend.Create;
using Core.CommendDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IAuthService
    {
        Task<string> Login(CreateLogin login);
        Task<string> RefreshToken(string token);
    }
}
