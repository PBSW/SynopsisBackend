namespace Shared.DTOs.Response;

public class ToDoListResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public List<ToDoItemResponse> Items { get; set; }
}