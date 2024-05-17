using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Shared;

namespace Application;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Item> _validator;
    
    public ItemService(IItemRepository itemRepository, IMapper mapper, IValidator<Item> validator)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
}