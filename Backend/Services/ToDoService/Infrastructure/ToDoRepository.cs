using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Shared;

namespace Infrastructure;

public class ToDoRepository : IToDoRepository
{
    private readonly DatabaseContext _context;
    public ToDoRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Task<ToDoList> CreateToDoListAsync(ToDoList toDoList)
    {
        throw new NotImplementedException();
    }
}