using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class ToDoRepository : IToDoRepository
{
    private readonly DatabaseContext _context;
    public ToDoRepository(DatabaseContext context)
    {
        _context = context;
    }
}