using DevBooking.Application.DTOs.Availability;

namespace DevBooking.Application.Interfaces;

public interface IAvailabilityService
{
    Task<AvailabilitySlotDto> CreateSlotAsync(string userId, CreateAvailabilitySlotRequest request);
    Task<List<AvailabilitySlotDto>> GetByDeveloperProfileIdAsync(int developerProfileId);
}