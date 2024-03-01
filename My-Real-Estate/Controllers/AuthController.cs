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
        public async Task<IActionResult> Register([FromBody] CreateUser user, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string registerId = await _authService.Register(user,role);

            if (registerId != "failed")
            {
                return Ok(new { UserId = registerId });
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
