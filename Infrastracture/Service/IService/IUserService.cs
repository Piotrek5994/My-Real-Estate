using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUserDto(UserFilter filter);
        Task<string> Register(CreateUserDto userDto, string role);
    }
}
