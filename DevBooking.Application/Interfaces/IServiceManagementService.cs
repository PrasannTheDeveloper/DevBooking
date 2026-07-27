using DevBooking.Application.DTOs.Service;

namespace DevBooking.Application.Interfaces;

public interface IServiceManagementService
{
    Task<ServiceDto> CreateServiceAsync(string userId, CreateServiceRequest request);
    Task<List<ServiceDto>> GetByDeveloperProfileIdAsync(int developerProfileId);
    Task<List<ServiceDto>> GetAllActiveAsync();
}