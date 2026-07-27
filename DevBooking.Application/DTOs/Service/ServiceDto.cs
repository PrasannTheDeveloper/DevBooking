namespace DevBooking.Application.DTOs.Service;

public class ServiceDto
{
    public int Id { get; set; }
    public int DeveloperProfileId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int EstimatedDurationDays { get; set; }
    public bool IsActive { get; set; }
}