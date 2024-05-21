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
        if (userCreate == null)
        {
            throw new NullReferenceException("UserCreate is null");
        }
        
        var user = _mapper.Map<User>(userCreate);
        
        user.DateCreated = DateTime.Now;
        
        var validationResult = await _validator.ValidateAsync(user);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }
        
        var createdUser = await _userRepository.CreateUserAsync(user);
        
        return _mapper.Map<UserResponse>(createdUser);
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        return _mapper.Map<List<UserResponse>>(await _userRepository.GetAllUsersAsynch());
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("id");
        }
        
        var user = await _userRepository.GetUserByIdAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return _mapper.Map<UserResponse>(user);
    }

    public Task<bool> DoesUserExistByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("id");
        }
        
        return _userRepository.DoesUserExistByIdAsync(id);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("id");
        }
        
        return await _userRepository.DeleteUserAsync(id);
    }
}