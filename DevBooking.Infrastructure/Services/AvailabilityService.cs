using DevBooking.Application.DTOs.Availability;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;

namespace DevBooking.Infrastructure.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAvailabilitySlotRepository _slotRepository;
    private readonly IDeveloperProfileRepository _profileRepository;

    public AvailabilityService(
        IAvailabilitySlotRepository slotRepository,
        IDeveloperProfileRepository profileRepository)
    {
        _slotRepository = slotRepository;
        _profileRepository = profileRepository;
    }

    public async Task<AvailabilitySlotDto> CreateSlotAsync(string userId, CreateAvailabilitySlotRequest request)
    {
        if (request.EndTime <= request.StartTime)
        {
            throw new InvalidOperationException("End time must be after start time.");
        }

        var profile = await _profileRepository.GetByUserIdAsync(userId);

        if (profile == null)
        {
            throw new InvalidOperationException("You must create a developer profile before adding availability.");
        }

        var overlaps = await _slotRepository.HasOverlappingSlotAsync(profile.Id, request.StartTime, request.EndTime);

        if (overlaps)
        {
            throw new InvalidOperationException("This slot overlaps with one of your existing slots.");
        }

        var slot = new AvailabilitySlot
        {
            DeveloperProfileId = profile.Id,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };

        await _slotRepository.AddAsync(slot);
        await _slotRepository.SaveChangesAsync();

        return new AvailabilitySlotDto
        {
            Id = slot.Id,
            DeveloperProfileId = slot.DeveloperProfileId,
            StartTime = slot.StartTime,
            EndTime = slot.EndTime,
            IsBooked = slot.IsBooked
        };
    }

    public async Task<List<AvailabilitySlotDto>> GetByDeveloperProfileIdAsync(int developerProfileId)
    {
        var slots = await _slotRepository.GetByDeveloperProfileIdAsync(developerProfileId);

        return slots.Select(s => new AvailabilitySlotDto
        {
            Id = s.Id,
            DeveloperProfileId = s.DeveloperProfileId,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            IsBooked = s.IsBooked
        }).ToList();
    }
}