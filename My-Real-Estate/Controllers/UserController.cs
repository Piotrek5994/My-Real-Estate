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
            return Json(new { Result = user });
        }
        return NotFound(new { Message = "User or Users don't found." }); ;
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
            return BadRequest(new { Message = "Registration failed." });
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user, string userId)
    {
        bool update = await _userService.UserUpdate(user, userId);
        if (!update)
        {
            return BadRequest(new { Message = "User update fail" });
        }
        return Ok(new { result = update });
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        bool delete = await _userService.UserDelete(userId);
        if (!delete)
        {
            return BadRequest(new { Message = "User does not exist." });
        }
        return Ok(new { Result = delete });
    }
}
