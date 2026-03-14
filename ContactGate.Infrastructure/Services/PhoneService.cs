using ContactGate.Application.DTOs;
using ContactGate.Application.Interfaces;
using ContactGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ContactGate.Infrastructure.Services;

// 
// Этот сервис предоставляет методы для управления телефонными номерами пользователей.В процессе создания и обновления номера выполняются проверки на существование пользователя и уникальность номера.
// 
public class PhoneService : IPhoneNumberService
{
    private readonly IApplicationDbContext _context;

    public PhoneService(IApplicationDbContext context)
    {
        _context = context;
    }

// 
// Метод который получает все телефонные номера из базы данных и возвращает их в виде списка DTO. 
// 
    public async Task<IEnumerable<PhoneNumberDto>> GetAllPhonesNumberAsync()
    {
        var phones = await _context.PhonesNumber.AsNoTracking().ToListAsync();

        return phones.Select(p => new PhoneNumberDto(
            p.Id, 
            p.Number, 
            p.UserId
        )).ToList();
    }

// 
// Метод который получает телефонные номера по ID пользователя из базы данных и возвращает их в виде списка DTO. 
// 
    
    public async Task<IEnumerable<PhoneNumberDto>> GetPhoneNumberByIdAsync(int userId)
    {
        var phones = await _context.PhonesNumber
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .ToListAsync();

        return phones.Select(p => new PhoneNumberDto(
            p.Id, 
            p.Number, 
            p.UserId
        )).ToList();
    }

// 
// Метод который создает новый телефонный номер для пользователя. 
    public async Task<PhoneNumberDto> CreatePhoneNumberAsync(CreatePhoneNumberDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists)
            throw new KeyNotFoundException($"Не удалось добавить номер: пользователь с ID {dto.UserId} не найден.");

        if (await _context.PhonesNumber.AnyAsync(p => p.Number == dto.Number))
            throw new InvalidOperationException($"Номер {dto.Number} уже существует в базе.");

        var phone = new PhoneNumber
        {
            Number = dto.Number,
            UserId = dto.UserId
        };

        _context.PhonesNumber.Add(phone);
        await _context.SaveChangesAsync();

        return new PhoneNumberDto(phone.Id, phone.Number, phone.UserId);
    }

// 
// Метод который обновляет существующий телефонный номер по его ID. 
// 
    public async Task UpdatePhoneNumberAsync(int id, UpdatePhoneNumberDto dto)
    {
        var phone = await _context.PhonesNumber.FindAsync(id);
        if (phone == null)
            throw new KeyNotFoundException($"Номер с ID {id} не найден.");

        if (await _context.PhonesNumber.AnyAsync(p => p.Number == dto.Number && p.Id != id))
            throw new InvalidOperationException($"Номер {dto.Number} уже занят другим пользователем.");

        phone.Number = dto.Number;
        await _context.SaveChangesAsync();
    }

// 
// Метод который удаляет телефонный номер по его ID. Если номер не найден, выбрасывает исключение.
// 
    public async Task DeletePhoneNumberAsync(int id)
    {
        var phone = await _context.PhonesNumber.FindAsync(id);
        if (phone == null)
            throw new KeyNotFoundException($"Номер с ID {id} не найден.");

        _context.PhonesNumber.Remove(phone);
        await _context.SaveChangesAsync();
    }
}