using EventPulse.Domain.Entities;
using EventPulse.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Infrastructure.Persistence;

internal class EventParticipantRepository : Repository<EventParticipant>, IEventParticipantRepository
{
    public EventParticipantRepository(DbContext dbContext) : base(dbContext)
    {
    }
}