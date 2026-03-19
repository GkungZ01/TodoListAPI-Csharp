using Microsoft.EntityFrameworkCore;
using TodoListAPI.Data;
using TodoListAPI.DTOs;
using TodoListAPI.Interfaces;
using TodoListAPI.Models;
namespace TodoListAPI.Service;

public class TodoService : ITodoService
{
    private readonly AppDbContext _context;

    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetTodoItemsByUserIdAsync(int userId)
    {
        return await _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem?> UpdateTodoItemAsync(int id, TodoItem updatedTodoItem)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return null;

        todoItem.Title = updatedTodoItem.Title;
        todoItem.Description = updatedTodoItem.Description;
        todoItem.IsCompleted = updatedTodoItem.IsCompleted;
        todoItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<bool> DeleteTodoItemAsync(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return false;

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return true;
    }

}
