using EventPulse.Domain.Entities;

namespace EventPulse.Infrastructure.Interfaces;

public interface IEventRepository : IRepository<Event>;