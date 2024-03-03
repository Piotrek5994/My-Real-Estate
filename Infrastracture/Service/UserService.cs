﻿using AutoMapper;
using Core.Commend;
using Core.CommendDto;
using Core.IRepositories;
using Core.Model;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;

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
        public async Task<List<UserDto>> GetUserDto(string userId)
        {
            var result = await _userRepository.GetUser(userId);
            var userDtos = _mapper.Map<List<UserDto>>(result);
            return userDtos;
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
