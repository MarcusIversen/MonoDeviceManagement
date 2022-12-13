using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutPasswordValidator: AbstractValidator<PutPasswordDTO>
{
    public PutPasswordValidator()
    {
        RuleFor(u => u.Id).NotEmpty().GreaterThan(0); 
        RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
    }
}