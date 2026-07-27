using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface IDeveloperProfileRepository
{
    Task<DeveloperProfile?> GetByIdAsync(int id);
    Task<DeveloperProfile?> GetByUserIdAsync(string userId);
    Task<List<DeveloperProfile>> GetAllAsync();
    Task AddAsync(DeveloperProfile profile);
    Task SaveChangesAsync();
}