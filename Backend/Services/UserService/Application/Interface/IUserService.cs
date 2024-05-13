using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application.Interface;

public interface IUserService
{
    public Task<UserResponse> CreateUserAsync(UserCreate userCreate);
}