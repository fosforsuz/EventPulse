using EventPulse.Domain.Entities;
using EventPulse.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPulse.Infrastructure.Persistence;

internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}