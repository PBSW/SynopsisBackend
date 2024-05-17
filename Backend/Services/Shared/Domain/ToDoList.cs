namespace Shared;

public class ToDoList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public List<Item> Items { get; set; }
}