using DevBooking.Application.DTOs.Review;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using DevBooking.Application.Exceptions;

namespace DevBooking.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookingRepository _bookingRepository;

        public ReviewService(
            IReviewRepository reviewRepository,
            IBookingRepository bookingRepository)
        {
            _reviewRepository = reviewRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<ReviewDto> CreateReviewAsync(string clientId, CreateReviewRequest request)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }

            if (booking.ClientId != clientId)
            {
                throw new ForbiddenException("You can only review your own bookings.");
            }

            if (booking.Status != BookingStatus.Completed)
            {
                throw new BusinessRuleException("You can only review a completed booking.");
            }

            var existingReview = await _reviewRepository.GetByBookingIdAsync(request.BookingId);
            if (existingReview != null)
            {
                throw new ConflictException("You have already reviewed this booking.");
            }

            if (request.Rating < 1 || request.Rating > 5)
            {
                throw new ValidationException("Rating must be between 1 and 5.");
            }

            var review = new Review
            {
                BookingId = booking.Id,
                DeveloperProfileId = booking.DeveloperProfileId,
                ClientId = clientId,
                Rating = request.Rating,
                Comment = request.Comment ?? string.Empty
            };

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return MapToDto(review);
        }

        public async Task<ReviewDto?> GetByBookingIdAsync(int bookingId)
        {
            var review = await _reviewRepository.GetByBookingIdAsync(bookingId);
            return review == null ? null : MapToDto(review);
        }

        public async Task<List<ReviewDto>> GetByDeveloperProfileIdAsync(int developerProfileId)
        {
            var reviews = await _reviewRepository.GetByDeveloperProfileIdAsync(developerProfileId);
            return reviews.Select(MapToDto).ToList();
        }

        public async Task DeleteReviewAsync(string clientId, int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);

            if (review == null)
            {
                throw new NotFoundException("Review not found.");
            }

            if (review.ClientId != clientId)
            {
                throw new ForbiddenException("You can only delete your own review.");
            }

            await _reviewRepository.DeleteAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task<double> GetAverageRatingAsync(int developerProfileId)
        {
            return await _reviewRepository.GetAverageRatingAsync(developerProfileId);
        }

        private static ReviewDto MapToDto(Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                BookingId = review.BookingId,
                DeveloperProfileId = review.DeveloperProfileId,
                ClientId = review.ClientId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }
        public async Task<ReviewDto> UpdateReviewAsync(string clientId, int reviewId, UpdateReviewRequest request)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);

            if (review == null)
            {
                throw new NotFoundException("Review not found.");
            }

            if (review.ClientId != clientId)
            {
                throw new ForbiddenException("You can only edit your own review.");
            }

            var editWindow = TimeSpan.FromDays(7);
            if (DateTime.UtcNow - review.CreatedAt > editWindow)
            {
                throw new BusinessRuleException("Reviews can only be edited within 7 days of posting.");
            }

            if (request.Rating < 1 || request.Rating > 5)
            {
                throw new ValidationException("Rating must be between 1 and 5.");
            }

            review.Rating = request.Rating;
            review.Comment = request.Comment ?? string.Empty;
            review.UpdatedAt = DateTime.UtcNow;

            await _reviewRepository.SaveChangesAsync();

            return MapToDto(review);
        }
    }
}