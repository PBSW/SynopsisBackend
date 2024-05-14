using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ToDoList> _validator;
    
    public ToDoService(IToDoRepository toDoRepository, IMapper mapper, IValidator<ToDoList> validator)
    {
        _toDoRepository = toDoRepository ?? throw new ArgumentNullException(nameof(toDoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<ToDoListResponse> CreateToDoListAsync(ToDoListCreate createList)
    {
        if (createList == null)
        {
            throw new NullReferenceException("ToDoListCreate is null");
        }
        
        var toDoList = _mapper.Map<ToDoList>(createList);
        
        var validationResult = await _validator.ValidateAsync(toDoList);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }
        
        var createdToDoList = await _toDoRepository.CreateToDoListAsync(toDoList);
        
        return _mapper.Map<ToDoListResponse>(createdToDoList);
    }
}