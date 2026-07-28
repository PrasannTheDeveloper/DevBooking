namespace DevBooking.Application.DTOs.Availability;

public class AvailabilitySlotDto
{
    public int Id { get; set; }
    public int DeveloperProfileId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsBooked { get; set; }
}