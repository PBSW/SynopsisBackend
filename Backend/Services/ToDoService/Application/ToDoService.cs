using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Shared;

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
}