using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Requests;

namespace Application.Interfaces;

public interface IAuthRepository
{
    public Task<bool> Register(AuthUser authUser);
    public Task<AuthUser> FindUserByUsername(string username);
    public Task<bool> IsAuthUser(string username);
}