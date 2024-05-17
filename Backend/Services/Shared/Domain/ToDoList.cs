namespace Shared;

public class ToDoList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public List<ToDoItem> Items { get; set; }
}