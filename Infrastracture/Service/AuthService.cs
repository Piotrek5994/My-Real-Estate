using Core.Commend;
using Core.IRepositories;
using Infrastracture.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<string> Register(CreateUser user, string role)
        {
            string result = role switch
            {
                string r when r.Contains("User") => await _authRepository.CreateUser(user),
                string r when r.Contains("Admin") => await _authRepository.CreateAdmin(user),
                _ => "Invalid role"
            };

            return result;
        }
    }
}
