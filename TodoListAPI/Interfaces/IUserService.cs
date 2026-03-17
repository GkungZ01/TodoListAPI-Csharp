namespace TodoListAPI.Interfaces;
using TodoListAPI.DTOs;

public interface IUserService
{
    Task<IEnumerable<UserResponsDto>> GetAll();
    Task<UserResponsDto> GetById(int id);
    Task<UserCreateDto> Create(UserCreateDto request);
}
