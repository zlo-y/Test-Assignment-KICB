using FluentValidation;
using ContactGate.Application.DTOs;
namespace ContactGate.Application.Validators;

// 
// Валидатор для CreateUserDto.Он проверяет, что имя не пустое и имеет допустимую длину, что email не пустой, имеет правильный формат и не слишком длинный, а также что дата рождения находится в прошлом.
// 
public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым.")
            .MinimumLength(2).WithMessage("Имя слишком короткое")
            .MaximumLength(100).WithMessage("Имя не может быть длиннее 100 символов.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email не может быть пустым.")
            .EmailAddress().WithMessage("Некорректный формат email.")
            .MaximumLength(255).WithMessage("Email не может быть длиннее 255 символов.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Дата рождения должна быть в прошлом.")
            .Must(date => date <= DateTime.Now.AddYears(-18))
            .WithMessage("Пользователь должен быть совершеннолетним (18+).");
    }
}