using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "กรุณากรอก Username")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username ต้องมีความยาว 3-50 ตัวอักษร")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอก Email")]
        [EmailAddress(ErrorMessage = "รูปแบบ Email ไม่ถูกต้อง")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอก Password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password ต้องมีอย่างน้อย 6 ตัวอักษร")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณายืนยัน Password")]
        [Compare("Password", ErrorMessage = "รหัสผ่านไม่ตรงกัน")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
