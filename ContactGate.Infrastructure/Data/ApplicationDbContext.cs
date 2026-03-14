using ContactGate.Application.Interfaces;
using ContactGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactGate.Infrastructure.Data;

// 
// Класс для взаимодействия с базой данных.
// 
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}

    public DbSet<User> Users => Set<User>();
    public DbSet<PhoneNumber> PhonesNumber => Set<PhoneNumber>();

// 
// Настройка модели данных. Здесь мы определяем ключи, обязательные поля, максимальную длину и связи между сущностями User и PhoneNumber. Например, указываем, что User может иметь много PhoneNumber.
// 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).IsRequired();
        });
        modelBuilder.Entity<PhoneNumber>(entity =>
        {   
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Number).IsRequired().HasMaxLength(20);

            entity.HasOne(e => e.User)
                .WithMany(u => u.PhonesNumber)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        base.OnModelCreating(modelBuilder);
    }


}