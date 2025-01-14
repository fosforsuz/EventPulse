using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
///     Middleware to limit the number of concurrent requests globally using a semaphore.
///     Helps prevent the system from being overwhelmed under high load.
/// </summary>
public class GlobalTrafficLimiterMiddleware
{
    /// <summary>
    ///     A semaphore to restrict the number of concurrent requests.
    ///     The maximum limit is set to 500 requests.
    /// </summary>
    private static readonly SemaphoreSlim Semaphore = new(500);

    private readonly RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GlobalTrafficLimiterMiddleware" /> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public GlobalTrafficLimiterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///     Middleware logic to limit the number of concurrent requests.
    ///     If the request cannot acquire the semaphore, it returns a 503 Service Unavailable response.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task Invoke(HttpContext context)
    {
        // Attempt to acquire the semaphore without waiting.
        if (!await Semaphore.WaitAsync(0))
        {
            // Semaphore can't be acquired, return 503 Service Unavailable response.
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.Headers.Append("Retry-After", "10"); // Suggest retrying after 10 seconds.
            var errorMessage = "The system is currently under high load. Please try again later.";
            var generalResponse = ResponseModel.Error(errorMessage).ToString();
            await context.Response.WriteAsync(generalResponse ?? string.Empty);
            return;
        }

        try
        {
            // Pass the request to the next middleware in the pipeline.
            await _next(context);
        }
        finally
        {
            // Ensure the semaphore is released even if an exception occurs.
            Semaphore.Release();
        }
    }
}