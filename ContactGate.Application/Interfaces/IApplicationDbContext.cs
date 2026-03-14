using Microsoft.EntityFrameworkCore;
using ContactGate.Domain.Entities;

namespace ContactGate.Application.Interfaces;

// 
// Интерфейс для доступа к базе данных.
// 
public interface IApplicationDbContext
{
    DbSet<User> Users {get;}
    DbSet<PhoneNumber> PhonesNumber {get;}

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}