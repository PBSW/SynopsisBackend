using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Create;

namespace API.Controller;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException("userService");
    }
    
    [HttpPost]
    [Route("api/user")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreate userCreate)
    {
        try
        {
            return Ok(_userService.CreateUserAsync(userCreate));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}