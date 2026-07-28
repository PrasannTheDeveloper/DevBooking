using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface IAvailabilitySlotRepository
{
    Task<AvailabilitySlot?> GetByIdAsync(int id);
    Task<List<AvailabilitySlot>> GetByDeveloperProfileIdAsync(int developerProfileId);

    // The key method for conflict detection — finds any slot for this developer
    // that overlaps a given time range
    Task<bool> HasOverlappingSlotAsync(int developerProfileId, DateTime startTime, DateTime endTime);

    Task AddAsync(AvailabilitySlot slot);
    Task SaveChangesAsync();
}