using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.DTOs.Requests;

namespace Infrastructure;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;

    public AuthRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    
    public async Task<bool> Register(AuthUser authUser)
    {
        await _context.AuthUsers.AddAsync(authUser);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AuthUser> FindUserByUsername(string username)
    {
        return await _context.AuthUsers.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<bool> IsAuthUser(string username)
    {
        return await _context.AuthUsers.AnyAsync(x => x.Username == username);
    }
}