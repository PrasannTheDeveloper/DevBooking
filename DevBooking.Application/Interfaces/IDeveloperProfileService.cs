using DevBooking.Application.DTOs.Developer;

namespace DevBooking.Application.Interfaces;

public interface IDeveloperProfileService
{
    Task<DeveloperProfileDto> CreateProfileAsync(string userId, CreateDeveloperProfileRequest request);
    Task<DeveloperProfileDto?> GetByUserIdAsync(string userId);
    Task<List<DeveloperProfileDto>> GetAllAsync();
}