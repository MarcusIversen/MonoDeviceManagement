using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutDeviceValidator : AbstractValidator<PutDeviceDTO>
{
    public PutDeviceValidator()
    {
        RuleFor(d => d.Id).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(d => d.SerialNumber).NotNull().NotEmpty();
        RuleFor(d => d.Amount).NotNull().NotEmpty();
        RuleFor(d => d.DeviceName).NotNull().NotEmpty();
    }
}