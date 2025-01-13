using System;
using System.Collections.Concurrent;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
/// Middleware to limit the rate of POST requests to the "/api/events/join" endpoint.
/// Helps prevent high load on the system by restricting requests from individual IPs.
/// </summary>
public class EventJoinTrafficLimiterMiddleware : IDisposable
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, RateLimitModal> _rateLimitDictionary = new();
    private readonly Timer _cleanupTimer;
    private bool _disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventJoinTrafficLimiterMiddleware"/> class.
    /// Sets up a timer to periodically clean up expired entries from the rate limit dictionary.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public EventJoinTrafficLimiterMiddleware(RequestDelegate next)
    {
        _next = next;
        // Timer to clean up entries older than 1 hour every hour.
        _cleanupTimer = new Timer(_ => CleanUpRateLimitDictionary(), null, 0, (int)TimeSpan.FromHours(1).TotalMilliseconds);
    }

    /// <summary>
    /// Middleware logic to enforce rate limits on POST requests to "/api/events/join".
    /// Returns a 503 Service Unavailable status if the limit is exceeded.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        // Extract request path and method for validation.
        var request = context.Request;
        var requestPath = request.Path.ToString();
        var requestMethod = request.Method;

        // Proceed to the next middleware if the request is not a POST to "/api/events/join".
        if (requestPath != "/api/events/join" || requestMethod != "POST")
        {
            await _next(context);
            return;
        }

        // Retrieve the client's IP address.
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // Check if the IP address already exists in the dictionary.
        if (_rateLimitDictionary.TryGetValue(ipAddress, out var rateLimitModal))
        {
            // If the request count exceeds the limit, return a 503 Service Unavailable response.
            if (rateLimitModal.RequestCount >= 2)
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
    /// Cleans up expired entries in the rate limit dictionary.
    /// Removes entries that have not been updated in the last 1 minute.
    /// </summary>
    private static void CleanUpRateLimitDictionary()
    {
        var now = DateTime.UtcNow;
        foreach (var (key, value) in _rateLimitDictionary)
        {
            if (now - value.LastRequest > TimeSpan.FromMinutes(1))
            {
                _rateLimitDictionary.TryRemove(key, out _);
            }
        }
    }

    /// <summary>
    /// Releases all resources used by the middleware, including the cleanup timer.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the managed and unmanaged resources used by the middleware.
    /// </summary>
    /// <param name="disposing">Indicates whether to release managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose of the cleanup timer.
                _cleanupTimer.Dispose();
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// Destructor to ensure resources are released if Dispose is not called.
    /// </summary>
    ~EventJoinTrafficLimiterMiddleware()
    {
        Dispose(false);
    }
}
