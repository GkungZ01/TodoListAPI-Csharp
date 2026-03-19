using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.DTOs;
using TodoListAPI.Interfaces;

namespace TodoListAPI.Controllers;

[Route("api/todos")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var todos = await _todoService.GetTodoItemsByUserIdAsync(userId);
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _todoService.GetTodoItemByIdAsync(id);
        if (todo == null)
            return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoItemCreateDto request)
    {
        var todoItem = new Models.TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            UserId = request.UserId
        };
        var createdTodo = await _todoService.CreateTodoItemAsync(todoItem);
        return Ok(createdTodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoItemUpdateDto request)
    {
        var updatedTodoItem = new Models.TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted
        };
        var updatedTodo = await _todoService.UpdateTodoItemAsync(id, updatedTodoItem);
        if (updatedTodo == null)
            return NotFound();
        return Ok(updatedTodo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _todoService.DeleteTodoItemAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}
