namespace EventPulse.Infrastructure.Modals;

public class AuthanticateResponse
{
    public AuthanticateResponse(string token, DateTime accessTokenExpireDate)
    {
        Token = token;
        AccessTokenExpireDate = accessTokenExpireDate;
    }

    public DateTime AccessTokenExpireDate { get; }
    public string Token { get; }
}