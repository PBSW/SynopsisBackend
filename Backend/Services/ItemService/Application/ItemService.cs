using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<List<Item>> _validator;
    
    public ItemService(IItemRepository itemRepository, IMapper mapper, IValidator<List<Item>> validator)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<List<ItemResponse>> CreateItemAsync(List<ItemCreate> items)
    {
        if (items == null)
        {
            throw new NullReferenceException("Item List cannot be null");
        }
        
        var itemsToCreate = _mapper.Map<List<Item>>(items);

        foreach (var item in itemsToCreate)
        {
            item.DateCreated = DateTime.Now;
        }
        
        var validationResult = await _validator.ValidateAsync(itemsToCreate);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }
        
        List<Item> itemsToReturn = await _itemRepository.CreateItemAsync(itemsToCreate);
        
        return _mapper.Map<List<ItemResponse>>(itemsToReturn);
    }

    public async Task<List<ItemResponse>> GetAllItemsAsync()
    {
        return _mapper.Map<List<ItemResponse>>(await _itemRepository.GetAllItemsAsync());
    }

    public async Task<ItemResponse> GetItemAsync(int id)
    {
        return _mapper.Map<ItemResponse>(await _itemRepository.GetItemAsync(id));
    }

    public async Task<List<ItemResponse>> GetAllItemsByToDoListIdAsync(int toDoListId)
    {
        return _mapper.Map<List<ItemResponse>>(await _itemRepository.GetAllItemsByToDoListIdAsync(toDoListId));
    }
}