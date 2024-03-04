using AutoMapper;
using Core.Commend.Create;
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
        public async Task<string> Login(CreateLogin login)
        {
            string result = await _authRepository.Login(login);
            return result;
        }
        public async Task<string> RefreshToken(string token)
        {
            string result = await _authRepository.Refresh(token);
            return result;
        }
    }
}
