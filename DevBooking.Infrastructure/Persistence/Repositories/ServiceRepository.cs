using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task<List<Service>> GetByDeveloperProfileIdAsync(int developerProfileId)
    {
        return await _context.Services
            .Where(s => s.DeveloperProfileId == developerProfileId)
            .ToListAsync();
    }

    public async Task<List<Service>> GetAllActiveAsync()
    {
        return await _context.Services
            .Where(s => s.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(Service service)
    {
        await _context.Services.AddAsync(service);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}