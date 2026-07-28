using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(int id);
    Task<List<Booking>> GetByClientIdAsync(string clientId);
    Task<List<Booking>> GetByDeveloperProfileIdAsync(int developerProfileId);
    Task AddAsync(Booking booking);
    Task<bool> TryBookSlotAsync(Booking booking, AvailabilitySlot slot);
    Task SaveChangesAsync();
}