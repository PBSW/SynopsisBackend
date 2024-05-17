using Shared;

namespace Application.Interfaces;

public interface IToDoRepository
{
    public Task<ToDoList> CreateToDoListAsync(ToDoList toDoList);
    public Task<ToDoList> GetToDoListAsync(int id);
    public Task<List<ToDoList>> GetAllToDoListsAsync();
    public Task<List<ToDoList>> GetAllListByUserIdAsync(int userId);
}