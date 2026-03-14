using FluentValidation;
using ContactGate.Application.DTOs;

namespace ContactGate.Application.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя обязательно для заполнения.")
            .MinimumLength(2).WithMessage("Имя должно содержать минимум 2 символа.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен.")
            .EmailAddress().WithMessage("Введите корректный адрес электронной почты.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Дата рождения обязательна.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Дата рождения не может быть в будущем.");
    }
}