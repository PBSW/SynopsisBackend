using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs;
using Shared.DTOs.Create;

namespace Application.Interfaces;

public interface IAuthService
{
    public Task<IActionResult> Login(AuthLogin dto);
    public Task<bool> Register(AuthCreate dto);
}