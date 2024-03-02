using Core.Commend;
using Core.CommendDto;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string registerId = await _authService.Register(userDto, role);

            if (registerId != "failed")
            {
                return Ok(new { UserId = registerId });
            }
            else
            {
                return BadRequest(new { message = "Registration failed" });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CreateLogin login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authService.Login(login);

            if (token == null)
            {
                return NotFound(new { messgae = "Login failed." });
            }

            return Ok(new { Token = token });
        }
        [HttpPut("Refresh")]
        public async Task<IActionResult> RefresToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Token is null or empty" });
            }
            string result = await _authService.RefreshToken(token);
            if (result == null)
            {
                return BadRequest(new { Message = "Invalid token sent" });
            }
            return Ok(new { Token = result });
        }
    }
}
