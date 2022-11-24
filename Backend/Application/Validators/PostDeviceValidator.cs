using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostDeviceValidator : AbstractValidator<PostDeviceDTO>
{
    public PostDeviceValidator()
    {
        RuleFor(d => d.SerialNumber).NotEmpty();
        RuleFor(d => d.Amount).NotEmpty();
        RuleFor(d => d.DeviceName).NotEmpty();
    }
}