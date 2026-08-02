using DevBooking.Application.DTOs.Review;

namespace DevBooking.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateReviewAsync(string clientId, CreateReviewRequest request);
        Task<ReviewDto?> GetByBookingIdAsync(int bookingId);
        Task<List<ReviewDto>> GetByDeveloperProfileIdAsync(int developerProfileId);
        Task DeleteReviewAsync(string clientId, int reviewId);
        Task<double> GetAverageRatingAsync(int developerProfileId);
        Task<ReviewDto> UpdateReviewAsync(string clientId, int reviewId, UpdateReviewRequest request);
    }
}