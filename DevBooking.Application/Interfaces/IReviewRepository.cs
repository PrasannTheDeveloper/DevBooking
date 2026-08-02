using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(int id);
        Task<Review?> GetByBookingIdAsync(int bookingId);
        Task<List<Review>> GetByDeveloperProfileIdAsync(int developerProfileId);

        Task AddAsync(Review review);
        Task DeleteAsync(Review review);

        Task<double> GetAverageRatingAsync(int developerProfileId);
        Task<int> GetReviewCountAsync(int developerProfileId);

        Task SaveChangesAsync();
    }
}