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
        _context.Database.EnsureCreated();
    }

    public async Task<ToDoList> CreateToDoListAsync(ToDoList toDoList)
    {
        await _context.ToDoLists.AddAsync(toDoList);
        await _context.SaveChangesAsync();
        
        if (toDoList.Id == 0)
        {
            throw new Exception("Failed to create ToDoList");
        }
        
        return toDoList;
    }
}