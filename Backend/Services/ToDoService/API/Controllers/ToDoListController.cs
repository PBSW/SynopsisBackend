using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs.Response;

namespace WebApplication1.Controllers;

[ApiController]
public class ToDoListController : ControllerBase
{
    private readonly IToDoService _toDoService;
    
    public ToDoListController(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ToDoListResponse>> CreateToDoList([FromBody] string title)
    {
        try
        {
            return Ok(await _toDoService.CreateToDoListAsync(title));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}