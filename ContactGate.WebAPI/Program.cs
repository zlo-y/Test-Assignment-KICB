using Microsoft.EntityFrameworkCore;
using ContactGate.Infrastructure.Data;
using ContactGate.Application.Interfaces;
using ContactGate.Infrastructure.Services; 
using ContactGate.WebAPI.Middleware;      
using FluentValidation;                   
using FluentValidation.AspNetCore;        
using ContactGate.Application.Validators; 

var builder = WebApplication.CreateBuilder(args);

// 1. БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());

// 2. Регистрация сервисов
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPhoneNumberService, PhoneService>();

// 3. Подключение Валидаторов
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Настройка CORS 
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// 5. Глобальный обработчик исключений
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.MapControllers();
app.Run();