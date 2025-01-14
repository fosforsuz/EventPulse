using System.Collections.Concurrent;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
///     Middleware to limit the rate of event search requests from a single IP address.
///     Helps prevent system overload by restricting frequent requests.
/// </summary>
public class EventSearchTrafficLimiterMiddleware : IDisposable
{
    private readonly Timer _cleanupTimer;
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, RateLimitModal> _rateLimitDictionary = new();
    private bool _disposed;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EventSearchTrafficLimiterMiddleware" /> class.
    ///     Sets up a timer to periodically clean up expired entries from the rate limit dictionary.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public EventSearchTrafficLimiterMiddleware(RequestDelegate next)
    {
        _next = next;
        // Timer cleans up expired entries every 1 minute.
        _cleanupTimer = new Timer(_ => CleanUpRateLimitDictionary(), null, 0,
            (int)TimeSpan.FromMinutes(1).TotalMilliseconds);
    }

    /// <summary>
    ///     Disposes the middleware resources, including the cleanup timer.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Middleware invocation logic to monitor and limit request rates per IP address.
    ///     Returns a 503 status code if the rate limit is exceeded.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task that represents the completion of request handling.</returns>
    public async Task Invoke(HttpContext context)
    {
        // Retrieve the client's IP address from the HTTP context.
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // Check if the IP address already exists in the dictionary.
        if (_rateLimitDictionary.TryGetValue(ipAddress, out var rateLimitModal))
        {
            // If the request count exceeds the limit, return a 503 Service Unavailable response.
            if (rateLimitModal.RequestCount >= 20)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                context.Response.Headers.Append("Retry-After", "60"); // Retry after 60 seconds.
                var errorMessage = "The system is currently under high load. Please try again later.";
                var generalResponse = ResponseModel.Error(errorMessage).ToString();
                await context.Response.WriteAsync(generalResponse ?? string.Empty);
                return;
            }

            // Increment the request count for the current IP address.
            rateLimitModal.Increment();
            _rateLimitDictionary[ipAddress] = rateLimitModal;
        }
        else
        {
            // Add a new entry for the IP address with an initial request count of 1.
            _rateLimitDictionary.TryAdd(ipAddress, new RateLimitModal(1, DateTime.UtcNow));
        }

        // Pass the request to the next middleware in the pipeline.
        await _next(context);
    }

    /// <summary>
    ///     Cleans up expired entries in the rate limit dictionary.
    ///     Removes entries that have not been updated in the last 1 minute.
    /// </summary>
    private void CleanUpRateLimitDictionary()
    {
        var now = DateTime.UtcNow;
        foreach (var key in _rateLimitDictionary.Keys)
            if ((now - _rateLimitDictionary[key].LastRequest).TotalMinutes > 1)
                _rateLimitDictionary.TryRemove(key, out _);
    }

    /// <summary>
    ///     Releases the managed and unmanaged resources used by the middleware.
    /// </summary>
    /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                // Dispose of the cleanup timer.
                _cleanupTimer.Dispose();

            _disposed = true;
        }
    }

    /// <summary>
    ///     Destructor to ensure resources are released if Dispose is not called.
    /// </summary>
    ~EventSearchTrafficLimiterMiddleware()
    {
        Dispose(false);
    }
}