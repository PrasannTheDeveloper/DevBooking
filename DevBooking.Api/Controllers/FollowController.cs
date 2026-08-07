using DevBooking.Application.Exceptions;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }

    private string CurrentUserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new UnauthorizedException("User ID not found in token.");

    // POST: api/follow/{userId}
    [HttpPost("{userId}")]
    public async Task<IActionResult> Follow(string userId, CancellationToken ct)
    {
        await _followService.FollowUserAsync(CurrentUserId, userId, ct);
        return NoContent();
    }

    // DELETE: api/follow/{userId}
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Unfollow(string userId, CancellationToken ct)
    {
        await _followService.UnfollowUserAsync(CurrentUserId, userId, ct);
        return NoContent();
    }

    // GET: api/follow/{userId}/status
    [HttpGet("{userId}/status")]
    public async Task<ActionResult<bool>> GetStatus(string userId, CancellationToken ct)
    {
        var isFollowing = await _followService.IsFollowingAsync(CurrentUserId, userId, ct);
        return Ok(isFollowing);
    }

    // GET: api/follow/{userId}/counts
    [HttpGet("{userId}/counts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCounts(string userId, CancellationToken ct)
    {
        var followers = await _followService.GetFollowersCountAsync(userId, ct);
        var following = await _followService.GetFollowingCountAsync(userId, ct);

        return Ok(new
        {
            Followers = followers,
            Following = following
        });
    }
}