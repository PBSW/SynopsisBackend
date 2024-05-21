using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;
using Shared.DTOs.Update;

namespace Application;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<List<Item>> _validator;
    private readonly IValidator<Item> _validatorItem;

    public ItemService(IItemRepository itemRepository, IMapper mapper, IValidator<List<Item>> validator, IValidator<Item> validatorItem)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _validatorItem = validatorItem ?? throw new ArgumentNullException(nameof(validatorItem));
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

    public async Task<ItemResponse> UpdateItemAsync(ItemUpdate item)
    {
        var itemToUpdate = _mapper.Map<Item>(item);
        
        var validationResult = await _validatorItem.ValidateAsync(itemToUpdate);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }
        
        itemToUpdate.DateUpdated = DateTime.Now;
        
        var updatedItem = await _itemRepository.UpdateItemAsync(itemToUpdate);
        
        return _mapper.Map<ItemResponse>(updatedItem);
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        return await _itemRepository.DeleteItemAsync(id);
    }
}