using SmartTestTask.Application.Exceptions;

namespace SmartTestTask.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            logger.LogWarning(exception, "Validation exception is occured");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An exception was thrown as a result of the request");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}