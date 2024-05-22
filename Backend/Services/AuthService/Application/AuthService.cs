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
using Shared.DTOs.Response;

namespace Application;

public class AuthService : IAuthService
{
    private const int keySize = 128 / 8;
    
    private readonly IJWTProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly IHttpRepository _httpRepo;
    
    
    public AuthService(
        IJWTProvider jwtProvider,
        IPasswordHasher passwordHasher,
        IAuthRepository authRepository,
        IMapper mapper,
        IHttpRepository httpRepo)
    {
        _jwtProvider = jwtProvider ?? throw new NullReferenceException("jwtProvider is null");
        _passwordHasher = passwordHasher ?? throw new NullReferenceException("passwordHasher is null");
        _authRepository = authRepository ?? throw new NullReferenceException("authRepository is null");
        _mapper = mapper ?? throw new NullReferenceException("mapper is null");
        _httpRepo = httpRepo ?? throw new NullReferenceException("httpRepository is null");
    }

    public async Task<IActionResult> Login(AuthLogin dto)
    {
        AuthUser authUser = await _authRepository.FindUserByUsername(dto.Username);
        
        if (authUser == null)
        {
            throw new ArgumentException("User not found");
        }
        
        bool isAuthenticated = await Authenticate(dto.PlainPassword, authUser);
        
        if (!isAuthenticated)
        {
            return await Task.FromResult<IActionResult>(new UnauthorizedResult());
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
        
        bool isAuthUser = await _authRepository.IsAuthUser(dto.Username);
        
        if (isAuthUser)
        {
            throw new ArgumentException("Login already exists");
        }
        
        AuthUser authUser = _mapper.Map<AuthCreate, AuthUser>(dto);
        
        UserCreate user = _mapper.Map<AuthCreate, UserCreate>(dto);
        
        UserResponse returnedUser = await _httpRepo.CreateUser(user);
        
        authUser.Salt = GenerateSalt();
        authUser.HashedPassword = await _passwordHasher.HashPassword(dto.PlainPassword, authUser.Salt);
        authUser.UserId = returnedUser.Id;
        
        return await _authRepository.Register(authUser);
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