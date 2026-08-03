using DevBooking.Application.DTOs.Service;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using DevBooking.Application.Exceptions;

namespace DevBooking.Infrastructure.Services;

public class ServiceManagementService : IServiceManagementService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IDeveloperProfileRepository _profileRepository;

    public ServiceManagementService(
        IServiceRepository serviceRepository,
        IDeveloperProfileRepository profileRepository)
    {
        _serviceRepository = serviceRepository;
        _profileRepository = profileRepository;
    }

    public async Task<ServiceDto> CreateServiceAsync(string userId, CreateServiceRequest request)
    {
        var profile = await _profileRepository.GetByUserIdAsync(userId);

        if (profile == null)
        {
            throw new BusinessRuleException("You must create a developer profile before adding services.");
        }

        var service = new Service
        {
            DeveloperProfileId = profile.Id,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            EstimatedDurationDays = request.EstimatedDurationDays
        };

        await _serviceRepository.AddAsync(service);
        await _serviceRepository.SaveChangesAsync();

        return MapToDto(service);
    }

    public async Task<List<ServiceDto>> GetByDeveloperProfileIdAsync(int developerProfileId)
    {
        var services = await _serviceRepository.GetByDeveloperProfileIdAsync(developerProfileId);
        return services.Select(MapToDto).ToList();
    }

    public async Task<List<ServiceDto>> GetAllActiveAsync()
    {
        var services = await _serviceRepository.GetAllActiveAsync();
        return services.Select(MapToDto).ToList();
    }

    private static ServiceDto MapToDto(Service service)
    {
        return new ServiceDto
        {
            Id = service.Id,
            DeveloperProfileId = service.DeveloperProfileId,
            Title = service.Title,
            Description = service.Description,
            Price = service.Price,
            EstimatedDurationDays = service.EstimatedDurationDays,
            IsActive = service.IsActive
        };
    }
}