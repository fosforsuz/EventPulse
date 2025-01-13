using System;
using System.Collections.Concurrent;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
/// Middleware to limit the rate of event search requests from a single IP address.
/// </summary>
public class EventSearchTrafficLimiterMiddleware
{
    private readonly RequestDelegate _next;
    // TODO: _cleanUpTimer is not being used. Remove it.
    private readonly Timer _cleanupTimer;
    private readonly ConcurrentDictionary<string, RateLimitModal> _rateLimitDictionary = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="EventSearchTrafficLimiterMiddleware"/> class.
    /// Sets up a timer to periodically clean up expired entries from the rate limit dictionary.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>

    public EventSearchTrafficLimiterMiddleware(RequestDelegate next)
    {
        _next = next;
        _cleanupTimer = new Timer(_ => CleanUpRateLimitDictionary(), null, 0, (int)TimeSpan.FromMinutes(1).TotalMilliseconds);
    }

    /// <summary>
    /// Middleware invocation logic to monitor and limit request rates per IP address.
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
            // If the request count exceeds the limit, return a 429 Too Many Requests response.
            if (rateLimitModal.RequestCount >= 20)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
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
    /// Removes entries older than 1 minute.
    /// </summary>
    private void CleanUpRateLimitDictionary()
    {
        var now = DateTime.UtcNow;
        foreach (var key in _rateLimitDictionary.Keys)
        {
            if ((now - _rateLimitDictionary[key].LastRequest).TotalMinutes > 1)
            {
                _rateLimitDictionary.TryRemove(key, out _);
            }
        }
    }
}
