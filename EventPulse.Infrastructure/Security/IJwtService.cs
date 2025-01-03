namespace EventPulse.Infrastructure.Security;

public interface IJwtService
{
    string GenerateToken(int userId, string role, string name, string email);
}