using Application.Interface;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _dbcontext;
    
    public UserRepository(DatabaseContext context)
    {
        _dbcontext = context;
        _dbcontext.Database.EnsureCreated();
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        // Check if the SSN already exists
        var existingPatient = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Mail == user.Mail);
        if (existingPatient != null)
        {
            throw new Exception("A user with the same Email already exists");
        }

        var entityEntry = await _dbcontext.Users.AddAsync(user);
        await _dbcontext.SaveChangesAsync();

        return entityEntry.Entity; // Access the Entity property to get the added patient entity
    }

    public async Task<List<User>> GetAllUsersAsynch()
    {
        return await _dbcontext.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<bool> DoesUserExistByIdAsync(int id)
    {
        return _dbcontext.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
            return false;
        
        // Patient exists, remove them
        _dbcontext.Users.Remove(user);
        int change = await _dbcontext.SaveChangesAsync();
        
        if (change == 0)
            return false;
        
        return true;
    }
}