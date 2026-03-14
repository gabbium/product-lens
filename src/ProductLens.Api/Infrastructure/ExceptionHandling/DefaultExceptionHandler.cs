using ProductLens.Domain.SeedWork;

namespace ProductLens.Api.Infrastructure.ExceptionHandling;

public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;

    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
    {
        _logger = logger;

        _exceptionHandlers = new()
        {
            { typeof(FluentValidation.ValidationException), HandleValidationException },
            { typeof(DomainException), HandleDomainException },
        };
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.TryGetValue(
            exceptionType,
            out Func<HttpContext, Exception, Task>? exceptionHandler))
        {
            await exceptionHandler.Invoke(httpContext, exception);
            return true;
        }

        _logger.LogError(exception, "An unhandled exception has occurred while executing the request");

        var problem = Results.Problem(
            title: "Application error",
            detail: "An unexpected error occurred.",
            type: "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1",
            statusCode: StatusCodes.Status500InternalServerError);

        await problem.ExecuteAsync(httpContext);

        return true;
    }

    private async Task HandleValidationException(
        HttpContext httpContext,
        Exception ex)
    {
        var exception = (FluentValidation.ValidationException)ex;

        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.CustomState is Error error ? error.Description : e.ErrorMessage).ToArray());

        var problem = Results.ValidationProblem(
            title: "Validation failure",
            detail: "One or more validation errors occurred",
            errors: errors,
            type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            statusCode: StatusCodes.Status400BadRequest);

        await problem.ExecuteAsync(httpContext);
    }

    private async Task HandleDomainException(
        HttpContext httpContext,
        Exception ex)
    {
        var exception = (DomainException)ex;

        var problem = Results.Problem(
            title: "Business rule violation",
            detail: exception.Error.Description,
            type: "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            statusCode: StatusCodes.Status422UnprocessableEntity);

        await problem.ExecuteAsync(httpContext);
    }
}
