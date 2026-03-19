using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.DTOs;

public class UserUpdateDto
{
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username ต้องมีความยาว 3-50 ตัวอักษร")]
    public string Username { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "รูปแบบ Email ไม่ถูกต้อง")]
    public string Email { get; set; } = string.Empty;
}
