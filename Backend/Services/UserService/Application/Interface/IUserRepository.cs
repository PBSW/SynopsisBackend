﻿using Shared;

namespace Application.Interface;

public interface IUserRepository
{
    public Task<User> CreateUserAsync(User user);
    public Task<List<User>> GetAllUsersAsynch();
    public Task<User> GetUserByIdAsync(int id);
    public Task<bool> DoesUserExistByIdAsync(int id);
    public Task<bool> DeleteUserAsync(int id);
}