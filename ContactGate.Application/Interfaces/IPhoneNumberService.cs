using ContactGate.Application.DTOs;
namespace ContactGate.Application.Interfaces;

// 
// Интрефейс для работы с телефонными номерами. Он определяет методы для получения всех телефонных номеров, получения телефонного номера по ID пользователя, создания, обновления и удаления телефонного номера.
// 
public interface IPhoneNumberService
{
    Task<IEnumerable<PhoneNumberDto>> GetAllPhonesNumberAsync();
    Task<IEnumerable<PhoneNumberDto>> GetPhoneNumberByIdAsync(int userId);
    Task<PhoneNumberDto> CreatePhoneNumberAsync(CreatePhoneNumberDto dto);
    Task UpdatePhoneNumberAsync(int id, UpdatePhoneNumberDto dto);
    Task DeletePhoneNumberAsync(int id);
}