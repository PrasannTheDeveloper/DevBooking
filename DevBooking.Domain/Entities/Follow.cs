namespace DevBooking.Domain.Entities;

public class Follow
{
    public int Id { get; set; }

    // The user doing the following
    public string FollowerId { get; set; } = string.Empty;

    // The user being followed
    public string FollowingId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}