﻿namespace Shared;

public class ToDoList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<ToDoItem> Items { get; set; }
}