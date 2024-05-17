using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IToDoService
{
    public Task<ToDoListResponse> CreateToDoListAsync(ToDoListCreate createList);
    public Task<ToDoListResponse> GetToDoListAsync(int id);
    public Task<List<ToDoListResponse>>  GetAllToDoListsAsync();
    public Task<List<ToDoListResponse>> GetAllListByUserIdAsync(int userId);
}