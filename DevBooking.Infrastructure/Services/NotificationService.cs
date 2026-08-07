using DevBooking.Application.Exceptions;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;

namespace DevBooking.Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IFollowRepository _followRepository;

    public NotificationService(
        INotificationRepository notificationRepository,
        IFollowRepository followRepository)
    {
        _notificationRepository = notificationRepository;
        _followRepository = followRepository;
    }

    public async Task CreateAsync(
        string userId,
        string title,
        string message,
        NotificationType type,
        int? bookingId,
        int? reviewId,
        CancellationToken ct)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            BookingId = bookingId,
            ReviewId = reviewId
        };

        await _notificationRepository.AddAsync(notification, ct);
        await _notificationRepository.SaveChangesAsync(ct);
    }

    public async Task NotifyFollowersAsync(
        string developerUserId,
        string title,
        string message,
        NotificationType type,
        CancellationToken ct)
    {
        var followerIds = await _followRepository.GetFollowerIdsAsync(developerUserId, ct);

        if (followerIds.Count == 0) return;

        var notifications = followerIds.Select(id => new Notification
        {
            UserId = id,
            Title = title,
            Message = message,
            Type = type
        });

        await _notificationRepository.AddRangeAsync(notifications, ct);
        await _notificationRepository.SaveChangesAsync(ct);
    }

    public async Task MarkAsReadAsync(int notificationId, string requestingUserId, CancellationToken ct)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId, ct);

        if (notification is null)
            throw new NotFoundException("Notification not found.");

        if (notification.UserId != requestingUserId)
            throw new BusinessRuleException("You cannot modify another user's notification.");

        notification.IsRead = true;
        await _notificationRepository.SaveChangesAsync(ct);
    }
}