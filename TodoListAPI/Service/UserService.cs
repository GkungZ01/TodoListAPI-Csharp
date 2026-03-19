using TodoListAPI.DTOs;
using TodoListAPI.Data;
using TodoListAPI.Interfaces;
using TodoListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponsDto>> GetAllAsync()
    {
        List<User> users = await _context.Users.ToListAsync();
        return users.Select(u => new UserResponsDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            CreatedAt = u.CreatedAt,
            IsActive = u.IsActive
        });
    }

    public async Task<UserResponsDto?> GetByIdAsync(int id)
    {
        User? user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        return new UserResponsDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            IsActive = user.IsActive
        };
    }

    public async Task<UserCreateDto> CreateAsync(UserCreateDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
        {
            throw new Exception("Username นี้มีอยู่แล้ว");
        }
        else if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new Exception("Email นี้มีอยู่แล้ว");
        }

        string PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = PasswordHash,
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserCreateDto
        {
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<UserResponsDto?> Update(int id, UserUpdateDto request)
    {
        User? user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username && u.Id != id))
            {
                throw new Exception("Username นี้มีอยู่แล้ว");
            }
            user.Username = request.Username;
        }
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.Id != id))
            {
                throw new Exception("Email นี้มีอยู่แล้ว");
            }
            user.Email = request.Email;
        }
        await _context.SaveChangesAsync();
        return new UserResponsDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive
        };
    }
}
