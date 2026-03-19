namespace TodoListAPI.DTOs;

public class TodoItemUpdateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}