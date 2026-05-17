using System.Net;
using FluentValidation;
using System.Text.Json;

namespace WebAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = (int)HttpStatusCode.InternalServerError;
        object message = "Sunucu tarafında bir hata oluştu.";

        if (exception is ValidationException validationException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            message = validationException.Errors.Select(x => x.ErrorMessage);
        }
        else if (exception is UnauthorizedAccessException unauthorizedException)
        {
            statusCode = (int)HttpStatusCode.Forbidden;
            message = unauthorizedException.Message;
        }
        else if (exception is Exception customException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            message = customException.Message;
        }

        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new
        {
            status = statusCode,
            errors = message
        });

        return context.Response.WriteAsync(result);
    }
}