using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using DevBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Notification notification, CancellationToken ct)
    {
        await _context.Notifications.AddAsync(notification, ct);
    }

    public async Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken ct)
    {
        await _context.Notifications.AddRangeAsync(notifications, ct);
    }

    public async Task<Notification?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == id, ct);
    }

    public async Task<List<Notification>> GetByUserIdAsync(
        string userId,
        bool? isRead,
        int page,
        int pageSize,
        CancellationToken ct)
    {
        var query = _context.Notifications
            .Where(n => n.UserId == userId);

        if (isRead.HasValue)
            query = query.Where(n => n.IsRead == isRead.Value);

        return await query
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public async Task<List<string>> GetFollowerIdsAsync(string followingId, CancellationToken ct)
    {
        return await _context.Follows
            .Where(f => f.FollowingId == followingId)
            .Select(f => f.FollowerId)
            .ToListAsync(ct);
    }

    public async Task<int> GetUnreadCountAsync(string userId, CancellationToken ct)
    {
        return await _context.Notifications
            .CountAsync(n => n.UserId == userId && !n.IsRead, ct);
    }

    public void Remove(Notification notification)
    {
        _context.Notifications.Remove(notification);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }
  
}