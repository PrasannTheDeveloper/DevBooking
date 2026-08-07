using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface INotificationService
{
    Task CreateAsync(
        string userId,
        string title,
        string message,
        NotificationType type,
        int? bookingId,
        int? reviewId,
        CancellationToken ct);

    Task NotifyFollowersAsync(
        string developerUserId,
        string title,
        string message,
        NotificationType type,
        CancellationToken ct);

    Task MarkAsReadAsync(int notificationId, string requestingUserId, CancellationToken ct);
}