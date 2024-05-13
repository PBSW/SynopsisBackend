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
            return Ok(await _userService.CreateUserAsync(userCreate));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/user")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            return Ok(await _userService.GetAllUsersAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/user/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}