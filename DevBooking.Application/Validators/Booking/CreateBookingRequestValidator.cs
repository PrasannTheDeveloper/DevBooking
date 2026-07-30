using DevBooking.Application.DTOs.Booking;
using FluentValidation;

namespace DevBooking.Application.Validators.Booking;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.DeveloperProfileId)
            .GreaterThan(0).WithMessage("A valid developer profile must be specified.");

        RuleFor(x => x)
            .Must(x => x.ServiceId.HasValue ^ x.AvailabilitySlotId.HasValue)
            .WithMessage("Provide exactly one of ServiceId or AvailabilitySlotId, not both or neither.");

        RuleFor(x => x.Notes)
            .MaximumLength(500);
    }
}