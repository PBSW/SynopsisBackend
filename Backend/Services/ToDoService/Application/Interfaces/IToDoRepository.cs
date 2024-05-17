using Shared;

namespace Application.Interfaces;

public interface IToDoRepository
{
    public Task<ToDoList> CreateToDoListAsync(ToDoList toDoList);
}