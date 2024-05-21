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
    private readonly IHttpRepository _httpRepository;
    
    public ToDoService(IToDoRepository toDoRepository, IMapper mapper, IValidator<ToDoList> validator, IHttpRepository httpRepository)
    {
        _toDoRepository = toDoRepository ?? throw new ArgumentNullException(nameof(toDoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _httpRepository = httpRepository ?? throw new ArgumentNullException(nameof(httpRepository));
    }

    public async Task<ToDoListResponse> CreateToDoListAsync(ToDoListCreate createList)
    {
        if (createList == null)
        {
            throw new NullReferenceException("ToDoListCreate is null");
        }
        
        var toDoList = _mapper.Map<ToDoList>(createList);
        
        toDoList.Items = new List<Item>();
        
        var validationResult = await _validator.ValidateAsync(toDoList);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }
        
        bool userExists = await _httpRepository.IsUser(toDoList.UserId);
        
        if (!userExists)
        {
            throw new ArgumentException("User does not exist");
        }
        
        var createdToDoList = await _toDoRepository.CreateToDoListAsync(toDoList);
        
        return _mapper.Map<ToDoListResponse>(createdToDoList);
    }

    public async Task<ToDoListResponse> GetToDoListAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Id is 0");
        }
        
        ToDoList toDoList = await _toDoRepository.GetToDoListAsync(id);

        ToDoListResponse returnList = _mapper.Map<ToDoListResponse>(toDoList);

        return returnList;
    }

    public async Task<List<ToDoListResponse>> GetAllToDoListsAsync()
    {
        List<ToDoList> toDoList = await _toDoRepository.GetAllToDoListsAsync();

        List<ToDoListResponse> returnList = _mapper.Map<List<ToDoListResponse>>(toDoList);

        return returnList;
    }

    public async Task<List<ToDoListResponse>> GetAllListByUserIdAsync(int userId)
    {
        List<ToDoList> toDoList = await _toDoRepository.GetAllListByUserIdAsync(userId);
        
        List<ToDoListResponse> returnList = _mapper.Map<List<ToDoListResponse>>(toDoList);
        
        return returnList;
    }
}