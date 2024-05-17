using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Create;
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
    [Route("api/todolist")]
    public async Task<ActionResult<ToDoListResponse>> CreateToDoList([FromBody] ToDoListCreate request)
    {
        try
        {
            return Ok(await _toDoService.CreateToDoListAsync(request));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/todolist/{id}")]
    public async Task<ActionResult<ToDoListResponse>> GetToDoList([FromRoute] int id)
    {
        try
        {
            return Ok(await _toDoService.GetToDoListAsync(id));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/todolist")]
    public async Task<ActionResult<List<ToDoListResponse>>> GetAllToDoLists()
    {
        try
        {
            return Ok(await _toDoService.GetAllToDoListsAsync());
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/todolist/user/{userId}")]
    public async Task<ActionResult<List<ToDoListResponse>>> GetAllListByUserId([FromRoute] int userId)
    {
        try
        {
            return Ok(await _toDoService.GetAllListByUserIdAsync(userId));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}