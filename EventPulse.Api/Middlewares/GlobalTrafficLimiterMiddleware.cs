using System;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
/// Middleware to limit the number of concurrent requests globally using a semaphore.
/// </summary>
public class GlobalTrafficLimiterMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly SemaphoreSlim _semaphore = new(500);

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalTrafficLimiterMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public GlobalTrafficLimiterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to process the HTTP request.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task Invoke(HttpContext context)
    {
        // If the semaphore is not available, return a 503 Service Unavailable response
        if (!await _semaphore.WaitAsync(0))
        {
            // semaphor can't be acquired, return 503
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            var errorMessage = "The system is currently under high load. Please try again later.";
            var generalResponse = ResponseModel.Error(errorMessage).ToString();
            await context.Response.WriteAsync(generalResponse ?? string.Empty);
            return;
        }

        try
        {
            // Access the next middleware in the pipeline
            await _next(context);
        }
        finally
        {
            // Release the semaphore when the request is done
            _semaphore.Release();
        }
    }
}
