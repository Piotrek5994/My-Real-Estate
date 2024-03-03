﻿using Core.CommendDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(string? userId = null)
        {
            var user = await _userService.GetUserDto(userId);
            if (user != null)
            {
                return Json(new { result = user });
            }
            return NotFound(new { message = "User or Users don't found." }); ;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto, string role)
        {
            string registerId = await _userService.Register(userDto, role);

            if (registerId != "failed")
            {
                return Ok(new { UserId = registerId });
            }
            else
            {
                return BadRequest(new { message = "Registration failed." });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok();
        }
    }
}
