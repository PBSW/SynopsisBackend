using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Create;
using Shared.DTOs.Update;

namespace API.Controllers;

[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    
    public ItemController(IItemService itemService)
    {
        _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
    }
    
    // Setters
    [HttpPost]
    [Route("api/items")]
    public async Task<IActionResult> CreateItem([FromBody] List<ItemCreate> items)
    {
        try
        {
            return Ok(await _itemService.CreateItemAsync(items));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    // Getters
    [HttpGet]
    [Route("api/items")]
    public async Task<IActionResult> GetAllItems()
    {
        try
        {
            return Ok(await _itemService.GetAllItemsAsync());
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/items/{id}")]
    public async Task<IActionResult> GetItem([FromRoute] int id)
    {
        try
        {
            return Ok(await _itemService.GetItemAsync(id));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/items/todolist/{toDoListId}")]
    public async Task<IActionResult> GetAllItemsByToDoListId([FromRoute] int toDoListId)
    {
        try
        {
            return Ok(await _itemService.GetAllItemsByToDoListIdAsync(toDoListId));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // Delete
    [HttpDelete]
    [Route("api/items/{id}")]
    public async Task<IActionResult> DeleteItem([FromRoute] int id)
    {
        try
        {
            return Ok(await _itemService.DeleteItemAsync(id));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // Update
    [HttpPut]
    [Route("api/items")]
    public async Task<IActionResult> UpdateItem([FromBody] ItemUpdate item)
    {
        try
        {
            return Ok(await _itemService.UpdateItemAsync(item));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}