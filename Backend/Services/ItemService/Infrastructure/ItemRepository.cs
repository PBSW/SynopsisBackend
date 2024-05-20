using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure;

public class ItemRepository : IItemRepository
{
    private readonly DatabaseContext _context;
    
    public ItemRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _context.Database.EnsureCreated();
    }
    
    public async Task<List<Item>> CreateItemAsync(List<Item> items)
    {
        var toDoListIds = items.Select(i => i.ToDoListId).Distinct();
        
        var existingToDoListIds = await _context.ToDoLists
            .Where(t => toDoListIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync();

        var itemsToAdd = new List<Item>();
        foreach (var item in items)
        {
            if (!existingToDoListIds.Contains(item.ToDoListId))
            {
                throw new Exception($"No ToDoList found with ID {item.ToDoListId}");
            }
            itemsToAdd.Add(item);
        }
        
        await _context.Items.AddRangeAsync(itemsToAdd);
        await _context.SaveChangesAsync();

        return itemsToAdd;
    }

    public async Task<List<Item>> GetAllItemsAsync()
    {
        return await _context.Items.ToListAsync();
    }
    
    public async Task<Item> GetItemAsync(int id)
    {
        return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Item>> GetAllItemsByToDoListIdAsync(int toDoListId)
    {
        return await _context.Items.Where(i => i.ToDoListId == toDoListId).ToListAsync();
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        
        if (item == null)
        {
            return false;
        }
        
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        
        return true;
    }
}