using AutoMapper;
using Core.Commend;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<string> Register(CreateUserDto userDto, string role)
        {
            CreateUser user = _mapper.Map<CreateUser>(userDto);
            string result = role switch
            {
                string r when r.Contains("User") => await _userRepository.CreateUser(user),
                string r when r.Contains("Admin") => await _userRepository.CreateAdmin(user),
                _ => "Invalid role"
            };

            return result;
        }
    }
}
