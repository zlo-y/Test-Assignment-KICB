using Microsoft.EntityFrameworkCore;
using ContactGate.Infrastructure.Data;
using ContactGate.Application.Interfaces;
using ContactGate.Infrastructure.Services; 
using ContactGate.WebAPI.Middleware;      
using FluentValidation;                   
using FluentValidation.AspNetCore;        
using ContactGate.Application.Validators; 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
// 
// 1. БД
// 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());
    
// 
// 2. Регистрация сервисов
// 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPhoneNumberService, PhoneService>();

// 
// 3. Подключение Валидаторов
// 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 
// 4. Настройка CORS 
// 
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>(); 
        System.Threading.Thread.Sleep(2000); 
        context.Database.Migrate();
        Console.WriteLine("База данных успешно обновлена (миграции применены).");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при подготовке базы данных: {ex.Message}");
    }
}

// 
// 5. Глобальный обработчик исключений
// 
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.MapControllers();
app.Run();