using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutDeviceValidator : AbstractValidator<PutDeviceDTO>
{
    public PutDeviceValidator()
    {
        RuleFor(d => d.Id).NotEmpty().GreaterThan(0);
        RuleFor(d => d.SerialNumber).NotEmpty()
            .WithState(x => throw new ArgumentException("Device serialNumber cannot be empty or null"));
        RuleFor(d => d.DeviceName).NotEmpty().WithState(x=> throw new ArgumentException("Device name cannot be empty or null"));
        RuleFor(d => d.Status).Must(d=> d.Equals("På lager") || d.Equals("I brug") || d.Equals("Defekt")).WithState(x=> throw new ArgumentException("Incorrect device status"));
    }
}