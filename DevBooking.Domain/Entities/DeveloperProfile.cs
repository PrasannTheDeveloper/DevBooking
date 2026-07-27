namespace DevBooking.Domain.Entities;

public class DeveloperProfile
{
    public int Id { get; set; }

    // Links to ApplicationUser.Id in Infrastructure — just a string, no class reference
    public string UserId { get; set; } = string.Empty;
    public string Headline { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public string TechStack { get; set; } = string.Empty; 
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; } = new List<AvailabilitySlot>();
}
public class Service
{
    public int Id { get; set; }
    public int DeveloperProfileId { get; set; }
    public DeveloperProfile DeveloperProfile { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int EstimatedDurationDays { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public class AvailabilitySlot
{
    public int Id { get; set; }

    public int DeveloperProfileId { get; set; }
    public DeveloperProfile DeveloperProfile { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public bool IsBooked { get; set; } = false;

    public Booking? Booking { get; set; }
}
public enum BookingStatus
{
    Pending,
    Confirmed,
    Completed,
    Cancelled
}

public class Booking
{
    public int Id { get; set; }
    public string ClientId { get; set; } = string.Empty;

    public int DeveloperProfileId { get; set; }
    public DeveloperProfile DeveloperProfile { get; set; } = null!;

    // Nullable — a booking might be against a fixed Service OR a time slot
    public int? ServiceId { get; set; }
    public Service? Service { get; set; }

    public int? AvailabilitySlotId { get; set; }
    public AvailabilitySlot? AvailabilitySlot { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}