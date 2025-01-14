using System.Collections.Concurrent;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
///     Middleware to limit the rate of POST requests to the "/api/events" endpoint per IP address.
/// </summary>
public class CreateEventTrafficLimiterMiddleware : IDisposable
{
    private readonly Timer _cleanupTimer;
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, RateLimitModal> _rateLimitDictionary = new();
    private bool _disposed;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateEventTrafficLimiterMiddleware" /> class.
    ///     Sets up a timer to periodically clean up expired entries from the rate limit dictionary.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public CreateEventTrafficLimiterMiddleware(RequestDelegate next)
    {
        _next = next;
        // Timer for cleaning up expired rate limit entries every 60 minutes.
        _cleanupTimer = new Timer(_ => CleanUpRateLimitDictionary(), null, 0,
            (int)TimeSpan.FromMinutes(60).TotalMilliseconds);
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
    ///     Middleware logic to enforce rate limits on POST requests to the "/api/events" endpoint.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task that represents the completion of request handling.</returns>
    public async Task Invoke(HttpContext context)
    {
        // Extract request path and method for validation.
        var request = context.Request;
        var requestPath = request.Path.ToString();
        var requestMethod = request.Method;

        // Proceed to the next middleware if the request is not a POST to "/api/events".
        if (requestPath != "/api/events" || requestMethod != "POST")
        {
            await _next(context);
            return;
        }

        // Retrieve the client's IP address.
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        // Check if the IP address already exists in the dictionary.
        if (_rateLimitDictionary.TryGetValue(ipAddress, out var rateLimitModal))
        {
            // If the request count exceeds the limit, return a 429 Too Many Requests response.
            if (rateLimitModal.RequestCount >= 15)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded.");
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
    ///     Removes entries that have not been updated in the last 60 minutes.
    /// </summary>
    private void CleanUpRateLimitDictionary()
    {
        var now = DateTime.UtcNow;
        foreach (var key in _rateLimitDictionary.Keys)
            if ((now - _rateLimitDictionary[key].LastRequest).TotalMinutes > 60)
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
    ~CreateEventTrafficLimiterMiddleware()
    {
        Dispose(false);
    }
}