using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IItemService
{
    Task<List<ItemResponse>> CreateItemAsync(List<ItemCreate> items);
    Task<List<ItemResponse>> GetAllItemsAsync();
    Task<ItemResponse> GetItemAsync(int id);
    Task<List<ItemResponse>> GetAllItemsByToDoListIdAsync(int toDoListId);
    Task<bool> DeleteItemAsync(int id);
}