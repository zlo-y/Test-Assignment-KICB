using ContactGate.Application.DTOs;
namespace ContactGate.Application.Interfaces;

// 
// Интерфейс для работы с пользователями. Он определяет методы для получения всех пользователей, получения пользователя по ID, создания, обновления и удаления пользователя.
// 
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);

    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    Task UpdateUserAsync(int id, UpdateUserDto dto);
    Task DeleteUserAsync(int id);
}