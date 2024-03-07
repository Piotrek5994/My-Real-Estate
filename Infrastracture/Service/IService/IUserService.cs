using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.ModelDto;

namespace Infrastracture.Service.IService;

public interface IUserService
{
    Task<List<UserDto>> GetUserDto(UserFilter filter);
    Task<string> Register(CreateUserDto userDto, string role);
    Task<bool> UserUpdate(UpdateUser user, string userId);
    Task<bool> UserDelete(string id);
}
