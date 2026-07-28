using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Persistence.Repositories;

public class AvailabilitySlotRepository : IAvailabilitySlotRepository
{
    private readonly ApplicationDbContext _context;

    public AvailabilitySlotRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AvailabilitySlot?> GetByIdAsync(int id)
    {
        return await _context.AvailabilitySlots.FindAsync(id);
    }

    public async Task<List<AvailabilitySlot>> GetByDeveloperProfileIdAsync(int developerProfileId)
    {
        return await _context.AvailabilitySlots
            .Where(s => s.DeveloperProfileId == developerProfileId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<bool> HasOverlappingSlotAsync(int developerProfileId, DateTime startTime, DateTime endTime)
    {
        return await _context.AvailabilitySlots
            .Where(s => s.DeveloperProfileId == developerProfileId)
            .AnyAsync(s => s.StartTime < endTime && s.EndTime > startTime);
    }

    public async Task AddAsync(AvailabilitySlot slot)
    {
        await _context.AvailabilitySlots.AddAsync(slot);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}