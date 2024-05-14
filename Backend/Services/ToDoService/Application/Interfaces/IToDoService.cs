using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IToDoService
{
    public Task<ToDoListResponse> CreateToDoListAsync(string title);
}