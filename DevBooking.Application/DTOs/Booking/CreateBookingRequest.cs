namespace DevBooking.Application.DTOs.Booking;

public class CreateBookingRequest
{
    public int DeveloperProfileId { get; set; }

    // Exactly one of these two must be set — validated in the service layer
    public int? ServiceId { get; set; }
    public int? AvailabilitySlotId { get; set; }

    public string? Notes { get; set; }
}