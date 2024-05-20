namespace Shared.DTOs.Response;

public class ItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public int ToDoListId { get; set; }
}