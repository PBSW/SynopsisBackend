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
}