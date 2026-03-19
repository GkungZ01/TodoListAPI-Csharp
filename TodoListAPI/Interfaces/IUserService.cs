namespace TodoListAPI.Interfaces;

using TodoListAPI.DTOs;

public interface IUserService
{
    Task<IEnumerable<UserResponsDto>> GetAllAsync();
    Task<UserResponsDto?> GetByIdAsync(int id);
    Task<UserCreateDto> CreateAsync(UserCreateDto request);
}

