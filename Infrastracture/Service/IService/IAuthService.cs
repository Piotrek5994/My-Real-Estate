using Core.Commend.Create;
using Core.Commend.Update;
using Core.CommendDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService;

public interface IAuthService
{
    Task<string> Login(CreateLogin login);
    Task<string> RefreshToken(string token);
    Task<bool> UpdateUserRole(string userId, string? role);
    Task<bool> ChangeUserPassword(UpdatePassword password);
}
