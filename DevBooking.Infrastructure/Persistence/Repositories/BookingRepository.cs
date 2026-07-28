using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Persistence.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings.FindAsync(id);
    }

    public async Task<List<Booking>> GetByClientIdAsync(string clientId)
    {
        return await _context.Bookings
            .Where(b => b.ClientId == clientId)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetByDeveloperProfileIdAsync(int developerProfileId)
    {
        return await _context.Bookings
            .Where(b => b.DeveloperProfileId == developerProfileId)
            .ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task<bool> TryBookSlotAsync(Booking booking, AvailabilitySlot slot)
    {
        slot.IsBooked = true;
        await _context.Bookings.AddAsync(booking);

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            _context.Entry(slot).State = EntityState.Unchanged;
            _context.Entry(booking).State = EntityState.Detached;
            return false;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}