using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostDeviceValidator : AbstractValidator<PostDeviceDTO>
{
    public PostDeviceValidator()
    {
        RuleFor(d => d.SerialNumber).NotEmpty()
            .WithState(x => throw new ArgumentException("Device serialNumber cannot be empty or null"));
        RuleFor(d => d.DeviceName).NotEmpty().WithState(x=> throw new ArgumentException("Device name cannot be empty or null"));
        RuleFor(d => d.Status).Matches("På lager|I brug|Defekt").WithState(x=> throw new ArgumentException("Incorrect device status"));
    }
}