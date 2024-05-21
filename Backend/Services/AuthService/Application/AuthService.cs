using System.Security.Claims;
using System.Security.Cryptography;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs;
using Shared.DTOs.Create;
using Shared.DTOs.Requests;

namespace Application;

public class AuthService : IAuthService
{
    private const int keySize = 128 / 8;
    
    private readonly IJWTProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    
    
    public AuthService(
        IJWTProvider jwtProvider,
        IPasswordHasher passwordHasher,
        IAuthRepository authRepository,
        IMapper mapper)
    {
        _jwtProvider = jwtProvider ?? throw new NullReferenceException("jwtProvider is null");
        _passwordHasher = passwordHasher ?? throw new NullReferenceException("passwordHasher is null");
        _authRepository = authRepository ?? throw new NullReferenceException("authRepository is null");
        _mapper = mapper ?? throw new NullReferenceException("mapper is null");
    }

    public async Task<IActionResult> Login(AuthLogin dto)
    {
        AuthUser authUser = await _authRepository.FindUser(dto.Username);
        
        if (authUser == null)
        {
            throw new ArgumentException("User not found");
        }
        
        bool isAuthenticated = await Authenticate(dto.PlainPassword, authUser);
        
        if (!isAuthenticated)
        {
            return await Task.FromResult<IActionResult>(new UnauthorizedResult());
        }
        
        // Generate a JWT token
        string jwtToken = GenerateAdminToken();
        
        // Get the user ID from the user service
        UserRequest user = await _authRepository.GetUserId(authUser.Username, jwtToken);
        
        if (user == null)
        {
            throw new NullReferenceException("User not found in user service");
        }
        
        string token = _jwtProvider.GenerateToken(authUser.UserId, authUser.Username);
        
        return await Task.FromResult<IActionResult>(new OkObjectResult(token));
    }

    public async Task<bool> Register(AuthCreate dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException("Dto is null");
        }
        
        AuthUser authUser = _mapper.Map<AuthCreate, AuthUser>(dto);
        
        authUser.Salt = GenerateSalt();
        authUser.HashedPassword = await _passwordHasher.HashPassword(dto.PlainPassword, authUser.Salt);
        
        UserCreate userDTO = _mapper.Map<AuthUser, UserCreate>(authUser);
        
        string jwtToken = GenerateAdminToken();
        
        return await _authRepository.Register(authUser, userDTO, jwtToken);
    }

    /*
     * Helper methods
     */
    private async Task<bool> Authenticate(string plainTextPassword, AuthUser AuthUser)
    {
        if (AuthUser == null)
        {
            return false;
        }
        
        var hashedPassword = await _passwordHasher.HashPassword(plainTextPassword, AuthUser.Salt);
        
        if (hashedPassword == AuthUser.HashedPassword)
        {
            return true;
        }
        throw new UnauthorizedAccessException("Invalid password");
    }
    
    private byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(keySize);
    }
    
    private string GenerateAdminToken()
    {
        var adminClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "admin")
        };

        // Assuming '1' is the admin user's ID and 'adminUser' is the username
        string adminToken = _jwtProvider.GenerateToken(1, "AuthService", adminClaims);
        return adminToken;
    }
}