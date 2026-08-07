using DevBooking.Application.Exceptions;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;

namespace DevBooking.Application.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;

    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task FollowUserAsync(
        string followerId,
        string followingId,
        CancellationToken ct)
    {
        if (followerId == followingId)
            throw new BusinessRuleException("You cannot follow yourself.");

        var alreadyFollowing = await _followRepository.IsFollowingAsync(
            followerId,
            followingId,
            ct);

        if (alreadyFollowing)
            throw new ConflictException("You are already following this user.");

        var follow = new Follow
        {
            FollowerId = followerId,
            FollowingId = followingId
        };

        await _followRepository.AddAsync(follow, ct);
        await _followRepository.SaveChangesAsync(ct);
    }

    public async Task UnfollowUserAsync(
        string followerId,
        string followingId,
        CancellationToken ct)
    {
        var follow = await _followRepository.GetFollowAsync(
            followerId,
            followingId,
            ct);

        if (follow is null)
            throw new NotFoundException("You are not following this user.");

        _followRepository.Remove(follow);
        await _followRepository.SaveChangesAsync(ct);
    }

    public async Task<bool> IsFollowingAsync(
        string followerId,
        string followingId,
        CancellationToken ct)
    {
        return await _followRepository.IsFollowingAsync(
            followerId,
            followingId,
            ct);
    }

    public async Task<int> GetFollowersCountAsync(
        string userId,
        CancellationToken ct)
    {
        return await _followRepository.GetFollowersCountAsync(userId, ct);
    }

    public async Task<int> GetFollowingCountAsync(
        string userId,
        CancellationToken ct)
    {
        return await _followRepository.GetFollowingCountAsync(userId, ct);
    }

}