namespace Shared.DTOs.Response;

public class ToDoItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
}