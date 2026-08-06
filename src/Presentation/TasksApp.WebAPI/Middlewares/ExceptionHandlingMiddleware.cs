using Microsoft.AspNetCore.Mvc;
using System.Net;
using TasksApp.Domain.Exceptions;

namespace TasksApp.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate _next, ILogger<ExceptionHandlingMiddleware> _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        var problemDetails = exception switch
        {
            UserException userEx => new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Error!",
                Detail = userEx.Message,
                Instance = context.Request.Path,
            },

            _ => LogAndGetGenericError(exception, context.Request.Path)
        };

        context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsJsonAsync(problemDetails);
    }

    private ProblemDetails LogAndGetGenericError(Exception exception, string path)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        return new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "An unexpected error occurred",
            Detail = "Please try again later or contact support if the problem persists.",
            Instance = path
        };
    }
}