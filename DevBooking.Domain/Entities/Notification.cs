using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Domain.Entities;

public enum NotificationType
{
    BookingCreated,
    BookingConfirmed,
    BookingCancelled,
    BookingCompleted,
    ReviewReceived,
    NewAvailability,
    System
}

public class Notification
{
    public int Id { get; set; }

    // ApplicationUser.Id
    public string UserId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public NotificationType Type { get; set; }

    // Optional navigation
    public int? BookingId { get; set; }
    public int? ReviewId { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}