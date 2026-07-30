using DevBooking.Application.DTOs.Availability;
using FluentValidation;

namespace DevBooking.Application.Validators.Availability;

public class CreateAvailabilitySlotRequestValidator : AbstractValidator<CreateAvailabilitySlotRequest>
{
    public CreateAvailabilitySlotRequestValidator()
    {
        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}