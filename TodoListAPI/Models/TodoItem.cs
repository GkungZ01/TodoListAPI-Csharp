using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListAPI.Models;

[Table("todo_items")]
public class TodoItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    [Column("description")]
    public string? Description { get; set; } = string.Empty;

    [Required]
    [Column("is_completed")]
    public bool IsCompleted { get; set; } = false;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
