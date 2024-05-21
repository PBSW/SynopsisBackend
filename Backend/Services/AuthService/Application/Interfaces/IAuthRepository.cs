using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Requests;

namespace Application.Interfaces;

public interface IAuthRepository
{
    public Task<bool> Register(AuthUser authUser, UserCreate userDTO, string JwtToken);
    public Task<AuthUser> FindUser(string username);
    public Task<UserRequest> GetUserId(string username, string JwtToken);
}