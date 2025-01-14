using EventPulse.Infrastructure.Context;
using EventPulse.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private ICategoryRepository? _categoryRepository;
    private bool _disposed;
    private IEventParticipantRepository? _eventParticipantRepository;

    private IEventRepository? _eventRepository;
    private INotificationRepository? _notificationRepository;
    private IUserRepository? _userRepository;

    public UnitOfWork(EventPulseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IEventRepository EventRepository => _eventRepository ??= new EventRepository(_context);

    public IEventParticipantRepository EventParticipantRepository =>
        _eventParticipantRepository ??= new EventParticipantRepository(_context);

    public INotificationRepository NotificationRepository =>
        _notificationRepository ??= new NotificationRepository(_context);

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            await _context.DisposeAsync();
            _disposed = true;
        }

        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _context.Dispose();

        _disposed = true;
    }
}