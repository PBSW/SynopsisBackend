namespace Shared.DTOs.Update;

public class ItemUpdate
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public int ToDoListId { get; set; }
}