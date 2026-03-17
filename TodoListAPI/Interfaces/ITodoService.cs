using TodoListAPI.Models;

namespace TodoListAPI.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetTodoItemsByUserIdAsync(int userId);
}
