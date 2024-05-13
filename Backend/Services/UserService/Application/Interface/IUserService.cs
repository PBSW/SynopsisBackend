using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application.Interface;

public interface IUserService
{
    public Task<UserResponse> CreateUserAsync(UserCreate userCreate);
    public Task<List<UserResponse>> GetAllUsersAsync();
    public Task<UserResponse> GetUserByIdAsync(int id);
}