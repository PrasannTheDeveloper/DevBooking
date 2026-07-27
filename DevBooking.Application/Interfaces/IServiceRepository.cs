using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface IServiceRepository
{
    Task<Service?> GetByIdAsync(int id);
    Task<List<Service>> GetByDeveloperProfileIdAsync(int developerProfileId);
    Task<List<Service>> GetAllActiveAsync();
    Task AddAsync(Service service);
    Task SaveChangesAsync();
}