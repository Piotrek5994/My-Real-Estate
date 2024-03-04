using Core.Commend.Create;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
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
        [HttpPut]
        public async Task<IActionResult> RefresToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Token is null or empty." });
            }
            string result = await _authService.RefreshToken(token);
            if (result == null)
            {
                return BadRequest(new { Message = "Invalid token sent." });
            }
            return Ok(new { Token = result });
        }
        [HttpPatch]
        [Route("/Auth/Role")]
        public async Task<IActionResult> ChangeRole(string userId, string? role = null)
        {
            var validRoles = new HashSet<string> { "Admin", "User" };

            if (role != null && !validRoles.Contains(role))
            {
                return BadRequest(new { message = $"Invalid role. Only the following roles are allowed: {string.Join(", ", validRoles)}" });
            }
            bool result = await _authService.UpdateUserRole(userId, role);
            if (!result)
            {
                return BadRequest(new { message = $"Incorrect role change for user : {userId}" });
            }
            return Ok(new { ChangeRole = role });
        }
        [HttpPatch]
        [Route("/Auth/Change")]
        public async Task<IActionResult> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest(new { message = "UserId, old password, and new password cannot be empty." });
            }

            bool result = await _authService.ChangeUserPassword(userId, oldPassword, newPassword);
            return Ok(new { ChangePassword = result });
        }
    }
}
