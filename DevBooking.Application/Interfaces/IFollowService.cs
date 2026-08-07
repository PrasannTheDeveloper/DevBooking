public interface IFollowService
{
    Task FollowUserAsync(string followerId, string followingId, CancellationToken ct);
    Task UnfollowUserAsync(string followerId, string followingId, CancellationToken ct);
    Task<bool> IsFollowingAsync(string followerId, string followingId, CancellationToken ct);
    Task<int> GetFollowersCountAsync(string userId, CancellationToken ct);
    Task<int> GetFollowingCountAsync(string userId, CancellationToken ct);
}