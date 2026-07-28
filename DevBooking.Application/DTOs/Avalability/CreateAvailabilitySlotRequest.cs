namespace DevBooking.Application.DTOs.Availability;

public class CreateAvailabilitySlotRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}