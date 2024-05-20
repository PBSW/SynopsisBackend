using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Create;

namespace API.Controllers;

[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    
    public ItemController(IItemService itemService)
    {
        _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
    }
    
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
}