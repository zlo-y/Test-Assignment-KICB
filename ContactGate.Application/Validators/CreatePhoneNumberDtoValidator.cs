using FluentValidation;
using ContactGate.Application.DTOs;
using System.Data;

namespace ContactGate.Application.Validators;

// 
// Валидатор для CreatePhoneNumberDto.
// 
public class CreatePhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public CreatePhoneNumberDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("ID юзера не может быть пустым.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Номер телефона не может быть пустым.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Некорректный формат номера (пример: +996555123456)");
    }
}