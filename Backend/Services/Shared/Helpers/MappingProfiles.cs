using AutoMapper;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Shared.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Auth
        CreateMap<AuthCreate, AuthUser>();

        // User
        CreateMap<UserCreate, User>();
        CreateMap<User, UserResponse>();
        
        // ToDoList
        CreateMap<ToDoListCreate, ToDoList>();
        CreateMap<ToDoList, ToDoListResponse>();
        
        // Item
        CreateMap<ItemCreate, Item>();
        CreateMap<Item, ItemResponse>();
    }
}