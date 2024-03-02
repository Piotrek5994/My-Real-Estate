using AutoMapper;
using Core.Commend;
using Core.CommendDto;
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
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository,IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<string> Register(CreateUserDto userDto, string role)
        {
            CreateUser user = _mapper.Map<CreateUser>(userDto);
            string result = role switch
            {
                string r when r.Contains("User") => await _authRepository.CreateUser(user),
                string r when r.Contains("Admin") => await _authRepository.CreateAdmin(user),
                _ => "Invalid role"
            };

            return result;
        }
        public async Task<string> Login(CreateLogin login)
        {
            string result = await _authRepository.Login(login);
            return result;
        }
    }
}
