namespace DevBooking.Domain.Entities;

public class Review
{
    public int Id { get; set; }

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int DeveloperProfileId { get; set; }
    public DeveloperProfile DeveloperProfile { get; set; } = null!;

    public string ClientId { get; set; } = string.Empty;

    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
