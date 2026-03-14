using ContactGate.Application.Interfaces;
using ContactGate.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using ContactGate.Domain.Entities;


namespace ContactGate.Infrastructure.Services;

// 
// Этот сервис реализует интерфейс IUserService и предоставляет методы для управления пользователями. Он использует IApplicationDbContext для взаимодействия с базой данных и выполняет операции CRUD (создание, чтение, обновление, удаление) для пользователей. Также он включает в себя проверки на уникальность email при создании и обновлении пользователя.
// 
public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;

    public UserService(IApplicationDbContext context)
    {
        _context = context;
    }

// 
// Метод который получает всех пользователей из базы данных, включая их телефонные номера!
// 
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users
            .Include(u => u.PhonesNumber)
            .AsNoTracking()
            .ToListAsync();

        return users.Select(u => new UserDto(
            u.Id, 
            u.Name, 
            u.Email, 
            u.DateOfBirth, 
            u.PhonesNumber.Select(p => new PhoneNumberDto(p.Id, p.Number, p.UserId)).ToList()
        ));
    }

// 
// Метод который получает пользователя по ID из базы данных, включая его телефонные номера. Если пользователь не найден, возвращает null.
// 
    public async Task<UserDto?> GetUserByIdAsync(int id)
{
    var user = await _context.Users
        .Include(u => u.PhonesNumber)
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Id == id);

    if (user == null) return null;

    return new UserDto(
        user.Id,
        user.Name,
        user.Email,
        user.DateOfBirth,
        user.PhonesNumber.Select(p => new PhoneNumberDto(p.Id, p.Number, p.UserId)).ToList()
    );
}

// 
// Метод который создает нового пользователя в базе данных. Он проверяет, что email уникален, и если все в порядке, добавляет пользователя и сохраняет изменения. Возвращает созданного пользователя в виде UserDto.
// 
    public async Task<UserDto> CreateUserAsync(CreateUserDto dto )
    {
        if(await _context.Users.AnyAsync(u => u.Email == dto.Email))
        {
            throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto(user.Id, user.Name, user.Email, user.DateOfBirth, new List<PhoneNumberDto>());
}

// 
// Метод который обновляет существующего пользователя в базе данных. Он проверяет, что пользователь существует и что новый email уникален (если он изменился). Если все в порядке, обновляет данные пользователя и сохраняет изменения. Если пользователь не найден, выбрасывает исключение.
// 
    public async Task UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if(user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }
        if(await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id))
        {
            throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.DateOfBirth = dto.DateOfBirth;

        await _context.SaveChangesAsync();
    }

// 
// Метод который удаляет пользователя из базы данных по ID. Он проверяет, что пользователь существует, и если да, удаляет его и сохраняет изменения. Если пользователь не найден, выбрасывает исключение.
// 
    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if(user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден.");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}