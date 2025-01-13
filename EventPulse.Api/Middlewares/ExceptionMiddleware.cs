using System.Net;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

/// <summary>
/// Middleware to handle exceptions globally in the application.
/// Catches unhandled exceptions and returns a standardized error response.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions.
    /// Passes the request to the next middleware and catches any exceptions thrown.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Proceed to the next middleware in the pipeline.
            await _next(context);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during request processing.
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles exceptions by returning a standardized error response.
    /// </summary>
    /// <param name="httpContext">The HTTP context for the current request.</param>
    /// <param name="ex">The exception that occurred.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        // Set the response content type and status code.
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Create a standardized error response message.
        var errorResponse = ResponseModel.Error(ex.Message)?.ToString() ?? "An error occurred";

        // Write the error response to the HTTP response body.
        return httpContext.Response.WriteAsync(errorResponse);
    }
}
