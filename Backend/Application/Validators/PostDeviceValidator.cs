using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostDeviceValidator : AbstractValidator<PostDeviceDTO>
{
    public PostDeviceValidator()
    {
        RuleFor(d => d.SerialNumber).NotEmpty();
        RuleFor(d => d.DeviceName).NotEmpty();
        RuleFor(d => d.Status).Matches("På lager|I brug|Defekt");
        RuleFor(d => d.RequestValue).Matches("IkkeSendt|Sendt|Accepteret");
    }
}