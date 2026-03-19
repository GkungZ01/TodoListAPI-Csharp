using Microsoft.EntityFrameworkCore;
using TodoListAPI.Models;

namespace TodoListAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");

        // Configure the User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.IsActive).IsRequired();
        });

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
        .Property(u => u.CreatedAt)
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<TodoItem>().ToTable("todo_items");

        modelBuilder.Entity<TodoItem>()
        .Property(u => u.CreatedAt)
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<TodoItem>()
        .Property(u => u.UpdatedAt)
        .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<TodoItem>()
        .HasOne(t => t.User)
        .WithMany(u => u.TodoItems)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);  // ลบ user → ลบ todo อัตโนมัติ
    }
}
