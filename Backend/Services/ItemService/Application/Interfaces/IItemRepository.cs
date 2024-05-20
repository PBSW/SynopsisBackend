using Shared;

namespace Application.Interfaces;

public interface IItemRepository
{
    public Task<List<Item>> CreateItemAsync(List<Item> items);
}