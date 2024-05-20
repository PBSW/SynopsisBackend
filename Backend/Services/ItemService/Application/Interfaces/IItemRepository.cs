using Shared;

namespace Application.Interfaces;

public interface IItemRepository
{
    public Task<List<Item>> CreateItemAsync(List<Item> items);
    public Task<List<Item>> GetAllItemsAsync();
    public Task<Item> GetItemAsync(int id);
    Task<List<Item>> GetAllItemsByToDoListIdAsync(int toDoListId);
    Task<bool> DeleteItemAsync(int id);
}