using DevBooking.Application.DTOs.Service;
using FluentValidation;

namespace DevBooking.Application.Validators.Service;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.EstimatedDurationDays)
            .GreaterThan(0).WithMessage("Estimated duration must be at least 1 day.");
    }
}