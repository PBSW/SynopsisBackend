﻿namespace Shared;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public int ToDoListId { get; set; }
    public bool IsDone { get; set; }
    
    public ToDoList ToDoList { get; set; }
}