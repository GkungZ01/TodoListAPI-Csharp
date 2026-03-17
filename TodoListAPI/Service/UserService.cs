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

    public async Task<IEnumerable<UserResponsDto>> GetAll()
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

    public async Task<UserResponsDto?> GetById(int id)
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

    public async Task<UserCreateDto> Create(UserCreateDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
        {
            throw new Exception("Username นี้มีอยู่แล้ว");
        } else if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new Exception("Email นี้มีอยู่แล้ว");
        } else if (request.Password != request.ConfirmPassword)
        {
            throw new Exception("Password และ Confirm Password ไม่ตรงกัน");
        } else if (request.Password.Length < 6)
        {
            throw new Exception("Password ต้องมีความยาวอย่างน้อย 6 ตัวอักษร");
        } else if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Email))
        {
            throw new Exception("Username และ Email ต้องไม่เป็นค่าว่าง");
        } else if (!request.Email.Contains("@"))
        {
            throw new Exception("Email ต้องมีรูปแบบที่ถูกต้อง");
        } else if (request.Username.Length < 3)
        {
            throw new Exception("Username ต้องมีความยาวอย่างน้อย 3 ตัวอักษร");
        } else if (request.Username.Length > 50)
        {
            throw new Exception("Username ต้องมีความยาวไม่เกิน 50 ตัวอักษร");
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
}
