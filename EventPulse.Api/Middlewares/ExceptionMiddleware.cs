using System.Net;
using EventPulse.Api.Models;

namespace EventPulse.Api.Middlewares;

public class ExceptionMiddleware
{

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorResponse = ResponseModel.Error("An error occurred")?.ToString() ?? "An error occurred";
        return httpContext.Response.WriteAsync(errorResponse);
    }
}
