using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventPulse.Infrastructure.Modals;
using Microsoft.IdentityModel.Tokens;

namespace EventPulse.Infrastructure.Security;

internal class JwtService : IJwtService
{
    private readonly string _audience;
    private readonly string _issuer;
    private readonly string _secret;

    public JwtService(string secret, string issuer, string audience)
    {
        _secret = secret;
        _issuer = issuer;
        _audience = audience;
    }

    public AuthanticateResponse GenerateToken(int userId, string role, string name, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expireDate = DateTime.Now.AddHours(1);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: expireDate,
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthanticateResponse(tokenString, expireDate);
    }
}