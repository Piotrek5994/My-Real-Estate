using AutoMapper;
using Core.Commend.Create;
using Core.CommendDto;
using Core.IRepositories;
using Infrastracture.Repositories;
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
        public async Task<string> Login(CreateLogin login) => await _authRepository.Login(login);
        public async Task<string> RefreshToken(string token) => await _authRepository.Refresh(token);
        public async Task<bool> UpdateUserRole(string userId,string? role) => await _authRepository.UpdateRole(userId,role);
        public async Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword) => await _authRepository.ChangePassword(userId,oldPassword,newPassword);
    }
}
