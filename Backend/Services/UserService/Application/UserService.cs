using Application.Interface;
using AutoMapper;
using FluentValidation;
using Shared;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<User> _validator;
    
    
    public UserService(IUserRepository userRepository, IMapper mapper, IValidator<User> validator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException("Value cannot be null. (Parameter 'userRepository')");
        _mapper = mapper ?? throw new ArgumentNullException("Value cannot be null. (Parameter 'mapper')");
        _validator = validator ?? throw new ArgumentNullException("Value cannot be null. (Parameter 'validator')");
    }
}