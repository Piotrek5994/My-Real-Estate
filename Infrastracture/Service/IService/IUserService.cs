using Core.CommendDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IUserService
    {
        Task<string> Register(CreateUserDto userDto, string role);
    }
}
