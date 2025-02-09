namespace EventPulse.Api.Models;

public class RateLimitModal
{
    private int _requestCount;

    public RateLimitModal(int count, DateTime lastRequest)
    {
        _requestCount = count;
        LastRequest = lastRequest;
        FirstRequest = DateTime.UtcNow;
    }

    public int RequestCount => _requestCount;
    public DateTime FirstRequest { get; private set; }
    public DateTime LastRequest { get; private set; }

    public void Increment()
    {
        Interlocked.Increment(ref _requestCount);
        LastRequest = DateTime.UtcNow;
    }
}