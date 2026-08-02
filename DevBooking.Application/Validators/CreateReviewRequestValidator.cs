using DevBooking.Application.DTOs.Review;
using FluentValidation;

namespace DevBooking.Application.Validators
{
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.BookingId)
                .GreaterThan(0).WithMessage("A valid booking must be specified.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(1000);
        }
    }
}