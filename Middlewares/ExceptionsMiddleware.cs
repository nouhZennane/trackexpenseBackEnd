using Azure;
using TrackExences.Exceptions;

namespace TrackExences.Middlewares;

public class ExceptionsMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            HandleExceptions(httpContext, e);
        }
    }

    private void HandleExceptions(HttpContext httpContext, Exception exception)
    {
        switch (exception)
        {
            case NotfoundException:
                httpContext.Response.StatusCode = 404;
                break;

            case ValidationException:
                httpContext.Response.StatusCode = 400;
                break;

            case UnauthorizedAccessException:
                httpContext.Response.StatusCode = 401;
                break;

            default:
                httpContext.Response.StatusCode = 500;
                break;
        }
        httpContext.Response.ContentType = "application/json";
        var response = new
        {
            message = exception.Message
        };
        httpContext.Response.WriteAsJsonAsync(response);
    }
}