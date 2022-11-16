using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutUserValidator : AbstractValidator<PutUserDTO>
{
    public PutUserValidator()
    {
        RuleFor(u => u.Id).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(8);
        RuleFor(u => u.FirstName).NotNull().NotEmpty();
        RuleFor(u => u.LastName).NotNull().NotEmpty();
        RuleFor(u => u.WorkNumber).NotNull().NotEmpty().MinimumLength(8);
    }
}