using System.ComponentModel.DataAnnotations;
namespace TodoListAPI.DTOs;

public class TodoItemCreateDto
{
    [Required(ErrorMessage = "กรุณากรอก Title")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title ต้องมีความยาว 3-100 ตัวอักษร")]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int UserId { get; set; }
}
