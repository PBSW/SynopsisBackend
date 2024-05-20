namespace Shared.DTOs.Response;

public class ToDoListResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public List<ItemResponse> Items { get; set; }
}