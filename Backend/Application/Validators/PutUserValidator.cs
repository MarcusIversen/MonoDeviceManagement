using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutUserValidator : AbstractValidator<PutUserDTO>
{
    public PutUserValidator()
    {
        RuleFor(u => u.Id).NotNull().NotEmpty().GreaterThan(0).WithState( u => throw new ArgumentException("Id cannot be null or less than 1"));
        RuleFor(u => u.Email).NotNull().NotEmpty().WithState(u => throw new ArgumentException("Email cannot be null or empty"));
        RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(8).WithState(u => throw new ArgumentException("Password cannot be null, empty and must have a minimum length greater than 8"));
        RuleFor(u => u.FirstName).NotNull().NotEmpty().WithState(u=> throw new ArgumentException("First name cannot be null or empty"));
        RuleFor(u => u.LastName).NotNull().NotEmpty().WithState(u => throw new ArgumentException("Last name cannot be null or empty"));
        RuleFor(u => u.WorkNumber).NotNull().NotEmpty().MinimumLength(8).WithState(u => throw new ArgumentException("Work number cannot be null, empty and must have a minimum length greater than 8"));
        RuleFor(u => u.Role).NotNull().NotEmpty().WithState(u => throw new ArgumentException("Role cannot be null or empty"));
    }
}