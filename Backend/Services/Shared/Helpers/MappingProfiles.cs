﻿using AutoMapper;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Shared.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // User
        CreateMap<UserCreate, User>();
        CreateMap<User, UserResponse>();
    }
}