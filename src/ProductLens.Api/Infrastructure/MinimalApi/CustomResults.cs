namespace ProductLens.Api.Infrastructure.MinimalApi;

public static class CustomResults
{
    public static IResult Problem(IOutcome outcome)
    {
        if (!outcome.IsError)
        {
            throw new InvalidOperationException("Cannot return Problem for a successful outcome.");
        }

        var errors = outcome.Errors;

        if (errors.Count > 1)
        {
            return Results.ValidationProblem(
                title: "Validation failure",
                detail: "One or more validation errors occurred",
                errors: ToValidationDictionary(errors),
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var firstError = errors[0];

        return Results.Problem(
            title: GetTitle(firstError.Type),
            detail: firstError.Description,
            type: GetType(firstError.Type),
            statusCode: GetStatusCode(firstError.Type)
        );
    }

    private static Dictionary<string, string[]> ToValidationDictionary(IReadOnlyList<Error> errors)
    {
        return errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).ToArray()
            );
    }

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Validation failure",
            ErrorType.BusinessRule => "Business rule violation",
            ErrorType.NotFound => "Resource not found",
            ErrorType.Conflict => "Resource conflict",
            _ => "Application error"
        };

    private static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.BusinessRule => "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1"
        };

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.BusinessRule => StatusCodes.Status422UnprocessableEntity,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
}
