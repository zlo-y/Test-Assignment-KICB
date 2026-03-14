using FluentValidation;
using ContactGate.Application.DTOs;

namespace ContactGate.Application.Validators;

// 
// Валидатор для UpdateUserDto.
// 
public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        // Те же правила, что и в CreateUserDtoValidator
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.DateOfBirth).NotEmpty().Must(date => date <= DateTime.Now.AddYears(-18));
    }
}