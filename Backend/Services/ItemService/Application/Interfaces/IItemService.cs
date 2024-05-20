using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IItemService
{
    Task<List<ItemResponse>> CreateItemAsync(List<ItemCreate> items);
}