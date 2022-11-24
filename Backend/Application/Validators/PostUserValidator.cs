using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostUserValidator : AbstractValidator<PostUserDTO>
{
    public PostUserValidator()
    {
        RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress().WithMessage(u=> throw new ArgumentException("Email cannot be null, empty and must be a valid email"));;
        RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(8);
        RuleFor(u => u.FirstName).NotNull().NotEmpty();
        RuleFor(u => u.LastName).NotNull().NotEmpty();
        RuleFor(u => u.WorkNumber).NotNull().NotEmpty().MinimumLength(8);
    }
}