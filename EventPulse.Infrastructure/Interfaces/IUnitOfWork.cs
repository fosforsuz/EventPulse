namespace EventPulse.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IEventRepository EventRepository { get; }
    IEventParticipantRepository EventParticipantRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}