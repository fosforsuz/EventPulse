using EventPulse.Domain.Entities;
using EventPulse.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Infrastructure.Persistence;

internal class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(DbContext dbContext) : base(dbContext)
    {
    }
}