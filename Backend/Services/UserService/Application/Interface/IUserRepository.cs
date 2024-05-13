using Shared;

namespace Application.Interface;

public interface IUserRepository
{
    public Task<User> CreateUserAsync(User user);
}