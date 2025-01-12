using EventPulse.Infrastructure.Modals;

namespace EventPulse.Infrastructure.Security;

public interface IJwtService
{
    AuthanticateResponse GenerateToken(int userId, string role, string name, string email);
}