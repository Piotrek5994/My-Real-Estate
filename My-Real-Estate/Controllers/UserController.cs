using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers;

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
    public async Task<IActionResult> GetUser([FromQuery] UserFilter filter)
    {
        var user = await _userService.GetUserDto(filter);
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
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user, string userId)
    {
        bool update = await _userService.UserUpdate(user, userId);
        if (!update)
        {
            return BadRequest(new { message = "User update fail" });
        }
        return Ok(new { result = update });
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        bool delete = await _userService.UserDelete(userId);
        if (!delete)
        {
            return BadRequest(new { message = "User does not exist." });
        }
        return Ok(new { result = delete });
    }
}
