﻿using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task<ToDoList> GetToDoListAsync(int id)
    {
        var toDoList = await _context.ToDoLists.Include(i => i.Items).Where(l => l.Id == id).FirstOrDefaultAsync();
        
        if (toDoList == null)
        {
            throw new Exception("ToDoList not found");
        }
        
        return toDoList;
    }

    public async Task<List<ToDoList>> GetAllToDoListsAsync()
    {
        return await _context.ToDoLists.Include(l => l.Items).ToListAsync();
    }

    public async Task<List<ToDoList>> GetAllListByUserIdAsync(int userId)
    {
        return _context.ToDoLists.Include(i => i.Items).Where(x => x.UserId == userId).ToList();
    }
}