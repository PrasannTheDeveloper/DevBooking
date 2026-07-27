namespace DevBooking.Application.DTOs.Service;

public class CreateServiceRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int EstimatedDurationDays { get; set; }
}