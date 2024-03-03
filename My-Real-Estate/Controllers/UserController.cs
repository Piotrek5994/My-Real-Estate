using Core.CommendDto;
using Infrastracture.Service;
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
        public async Task<IActionResult> GetUser()
        {
            return View();
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
                return BadRequest(new { message = "Registration failed" });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return View();
        }
    }
}
