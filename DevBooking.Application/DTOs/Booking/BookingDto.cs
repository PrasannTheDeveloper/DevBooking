namespace DevBooking.Application.DTOs.Booking;

public class BookingDto
{
    public int Id { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public int DeveloperProfileId { get; set; }
    public int? ServiceId { get; set; }
    public int? AvailabilitySlotId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}