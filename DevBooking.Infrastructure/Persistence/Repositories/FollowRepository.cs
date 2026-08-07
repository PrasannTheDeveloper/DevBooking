using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using DevBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Persistence.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Follow?> GetFollowAsync(string followerId, string followingId, CancellationToken ct)
    {
        return await _context.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId, ct);
    }

    public async Task AddAsync(Follow follow, CancellationToken ct)
    {
        await _context.Follows.AddAsync(follow, ct);
    }

    public void Remove(Follow follow)
    {
        _context.Follows.Remove(follow);
    }

    public async Task<int> GetFollowersCountAsync(string userId, CancellationToken ct)
    {
        return await _context.Follows.CountAsync(f => f.FollowingId == userId, ct);
    }

    public async Task<int> GetFollowingCountAsync(string userId, CancellationToken ct)
    {
        return await _context.Follows.CountAsync(f => f.FollowerId == userId, ct);
    }

    public async Task<List<Follow>> GetFollowersAsync(string userId, CancellationToken ct)
    {
        return await _context.Follows
            .Where(f => f.FollowingId == userId)
            .ToListAsync(ct);
    }

    public async Task<bool> IsFollowingAsync(string followerId, string followingId, CancellationToken ct)
    {
        return await _context.Follows
            .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId, ct);
    }
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }
    public async Task<List<string>> GetFollowerIdsAsync(string followingId, CancellationToken ct)
    {
        return await _context.Follows
            .Where(f => f.FollowingId == followingId)
            .Select(f => f.FollowerId)
            .ToListAsync(ct);
    }
}