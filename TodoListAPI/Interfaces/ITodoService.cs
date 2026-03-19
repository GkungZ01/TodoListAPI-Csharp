using TodoListAPI.Models;

namespace TodoListAPI.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetTodoItemsByUserIdAsync(int userId);
    Task<TodoItem?> GetTodoItemByIdAsync(int id);
    Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
    Task<TodoItem?> UpdateTodoItemAsync(int id, TodoItem updatedTodoItem);
    Task<bool> DeleteTodoItemAsync(int id);
}
