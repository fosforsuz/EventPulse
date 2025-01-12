using System;

namespace EventPulse.Infrastructure.Modals;

public class AuthanticateResponse
{
    public DateTime AccessTokenExpireDate { get; }
    public string Token { get; }

    public AuthanticateResponse(string token, DateTime accessTokenExpireDate)
    {
        Token = token;
        AccessTokenExpireDate = accessTokenExpireDate;
    }
}
