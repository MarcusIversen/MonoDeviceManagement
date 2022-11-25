using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostUserValidator : AbstractValidator<PostUserDTO>
{
    public PostUserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage(u=> throw new ArgumentException("Email cannot be null, empty and must be a valid email"));;
        RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.LastName).NotEmpty();
        RuleFor(u => u.WorkNumber).NotEmpty().MinimumLength(8);
        RuleFor(u => u.Role).NotEmpty().IsInEnum();
    }
}