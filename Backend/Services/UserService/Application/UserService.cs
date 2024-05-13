using Application.Interface;
using AutoMapper;
using FluentValidation;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<User> _validator;
    
    
    public UserService(IUserRepository userRepository, IMapper mapper, IValidator<User> validator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException("userRepository");
        _mapper = mapper ?? throw new ArgumentNullException("mapper");
        _validator = validator ?? throw new ArgumentNullException("validator");
    }

    public async Task<UserResponse> CreateUserAsync(UserCreate userCreate)
    {
        throw new NotImplementedException();
    }
}