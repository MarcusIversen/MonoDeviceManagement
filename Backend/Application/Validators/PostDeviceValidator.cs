using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostDeviceValidator : AbstractValidator<PostDeviceDTO>
{
    public PostDeviceValidator()
    {
        RuleFor(d => d.SerialNumber).NotNull().NotEmpty();
        RuleFor(d => d.Amount).NotNull().NotEmpty();
        RuleFor(d => d.DeviceName).NotNull().NotEmpty();
    }
}