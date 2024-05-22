using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IHttpRepository
{
    public Task<UserResponse> CreateUser(UserCreate userCreate);
}