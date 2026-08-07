using DevBooking.Domain.Entities;

namespace DevBooking.Application.Interfaces;

public interface INotificationRepository
{
    Task AddAsync(Notification notification, CancellationToken ct);

    Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken ct);

    Task<Notification?> GetByIdAsync(int id, CancellationToken ct);

    Task<List<Notification>> GetByUserIdAsync(
        string userId,
        bool? isRead,
        int page,
        int pageSize,
        CancellationToken ct);

    Task<int> GetUnreadCountAsync(string userId, CancellationToken ct);

    void Remove(Notification notification);

    Task SaveChangesAsync(CancellationToken ct);
    Task<List<string>> GetFollowerIdsAsync(string followingId, CancellationToken ct);

}