using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListAPI.Models;

[Table("users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column("passwordHash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
