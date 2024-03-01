using Core.Commend;
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
        public async Task<IActionResult> Register([FromBody] CreateUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = await _authService.RegisterUser(user);

            if (userId != "failed")
            {
                return Ok(new { UserId = userId });
            }
            else
            {
                return BadRequest(new { message = "Registration failed" });
            }
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> RefresToken()
        {
            return View();
        }
    }
}
