using DevBooking.Application.DTOs.Client;

namespace DevBooking.Application.Interfaces
{
    public interface IClientProfileService
    {
        Task<ClientProfileDto> CreateProfileAsync(string userId, CreateClientProfileRequest request);
        Task<ClientProfileDto?> GetByUserIdAsync(string userId);
        Task<List<ClientProfileDto>> GetAllAsync();
    }
}
