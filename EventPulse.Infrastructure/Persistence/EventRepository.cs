using EventPulse.Domain.Entities;
using EventPulse.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Infrastructure.Persistence;

internal class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(DbContext dbContext) : base(dbContext)
    {
    }
}