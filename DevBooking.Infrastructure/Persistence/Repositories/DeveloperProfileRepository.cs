using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Persistence.Repositories;

public class DeveloperProfileRepository : IDeveloperProfileRepository
{
    private readonly ApplicationDbContext _context;

    public DeveloperProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeveloperProfile?> GetByIdAsync(int id)
    {
        return await _context.DeveloperProfiles.FindAsync(id);
    }

    public async Task<DeveloperProfile?> GetByUserIdAsync(string userId)
    {
        return await _context.DeveloperProfiles
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<List<DeveloperProfile>> GetAllAsync()
    {
        return await _context.DeveloperProfiles.ToListAsync();
    }

    public async Task AddAsync(DeveloperProfile profile)
    {
        await _context.DeveloperProfiles.AddAsync(profile);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}