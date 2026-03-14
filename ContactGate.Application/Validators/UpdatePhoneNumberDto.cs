using FluentValidation;
using ContactGate.Application.DTOs;

namespace ContactGate.Application.Validators;

// 
// валидатор для UpdatePhoneNumberDto.
// 
public class UpdatePhoneNumberDtoValidator : AbstractValidator<UpdatePhoneNumberDto>
{
    public UpdatePhoneNumberDtoValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Номер телефона не может быть пустым.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Некорректный формат номера. Номер должен содержать от 10 до 15 цифр и может начинаться с +.");
    }
}