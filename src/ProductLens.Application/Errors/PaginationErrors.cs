namespace ProductLens.Application.Errors;

public static class PaginationErrors
{
    public static Error PageNumberInvalid(int min) =>
        Error.Validation(
            code: "Product.PageNumber.Invalid",
            description: $"Page number must be greater than or equal to {min}.",
            metadata: new Dictionary<string, object>
            {
                ["min"] = min
            });

    public static Error PageSizeOutOfRange(int min, int max) =>
        Error.Validation(
            code: "Product.PageSize.OutOfRange",
            description: $"Page size must be between {min} and {max}.",
            metadata: new Dictionary<string, object>
            {
                ["min"] = min,
                ["max"] = max
            });
}

