using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface IFollowRepository
{
    Task<Follow?> GetFollowAsync(string followerId, string followingId, CancellationToken ct);
    Task AddAsync(Follow follow, CancellationToken ct);
    void Remove(Follow follow);
    Task<int> GetFollowersCountAsync(string userId, CancellationToken ct);
    Task<int> GetFollowingCountAsync(string userId, CancellationToken ct);
    Task<List<Follow>> GetFollowersAsync(string userId, CancellationToken ct);
    Task<bool> IsFollowingAsync(string followerId, string followingId, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    Task<List<string>> GetFollowerIdsAsync(string followingId, CancellationToken ct);
}