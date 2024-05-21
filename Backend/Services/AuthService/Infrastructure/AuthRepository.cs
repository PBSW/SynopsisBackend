using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Shared;
using Shared.DTOs.Create;
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
    
    public async Task<bool> Register(AuthUser authUser, UserCreate userDTO, string jwtToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task<AuthUser> FindUser(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<UserRequest> GetUserId(string username, string jwtToken)
    {
        throw new NotImplementedException();
    }
}