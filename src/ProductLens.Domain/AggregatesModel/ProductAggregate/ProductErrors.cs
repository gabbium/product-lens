namespace ProductLens.Domain.AggregatesModel.ProductAggregate;

public static class ProductErrors
{
    private const string ProductIdKey = "productId";
    private const string MaxLengthKey = "maxLength";
    private const string MinKey = "min";

    public static Error NotFound(Guid id) =>
       Error.NotFound(
           code: "Product.NotFound",
           description: "Product was not found.",
           metadata: new Dictionary<string, object>
           {
               [ProductIdKey] = id
           });

    public static Error IdRequired() =>
        Error.Validation(
            code: "Product.Id.Required",
            description: "Product id is required.");

    public static Error NameRequired() =>
        Error.Validation(
            code: "Product.Name.Required",
            description: "Product name is required.");

    public static Error NameTooLong(int maxLength) =>
        Error.Validation(
            code: "Product.Name.MaxLengthExceeded",
            description: $"Product name must not exceed {maxLength} characters.",
            metadata: new Dictionary<string, object>
            {
                [MaxLengthKey] = maxLength
            });

    public static Error DescriptionTooLong(int maxLength) =>
        Error.Validation(
            code: "Product.Description.MaxLengthExceeded",
            description: $"Product description must not exceed {maxLength} characters.",
            metadata: new Dictionary<string, object>
            {
                [MaxLengthKey] = maxLength
            });

    public static Error PriceMustBeGreaterThan(decimal min) =>
        Error.Validation(
            code: "Product.Price.MustBeGreaterThan",
            description: $"Product price must be greater than {min}.",
            metadata: new Dictionary<string, object>
            {
                [MinKey] = min
            });

    public static Error CurrencyRequired() =>
        Error.Validation(
            code: "Product.Currency.Required",
            description: "Currency is required.");

    public static Error CurrencyMustBeIsoCode() =>
        Error.Validation(
            code: "Product.Currency.MustBeIsoCode",
            description: "Currency must be a valid ISO code.");

    public static Error ModificationNotAllowedForDiscontinued(Guid id) =>
        Error.BusinessRule(
            code: "Product.Modification.NotAllowedForDiscontinued",
            description: "Modification is not allowed for a discontinued product.",
            metadata: new Dictionary<string, object>
            {
                [ProductIdKey] = id
            });

    public static Error ActivationNotAllowedForDiscontinued(Guid id) =>
        Error.BusinessRule(
            code: "Product.Activation.NotAllowedForDiscontinued",
            description: "Activation is not allowed for a discontinued product.",
            metadata: new Dictionary<string, object>
            {
                [ProductIdKey] = id
            });

    public static Error DiscontinueNotAllowedForDraft(Guid id) =>
        Error.BusinessRule(
            code: "Product.Discontinue.NotAllowedForDraft",
            description: "A draft product cannot be discontinued.",
            metadata: new Dictionary<string, object>
            {
                [ProductIdKey] = id
            });

    public static Error DeleteNotAllowedForNonDraft(Guid id) =>
        Error.BusinessRule(
            code: "Product.Delete.NotAllowedForNonDraft",
            description: "Only draft products can be deleted.",
            metadata: new Dictionary<string, object>
            {
                [ProductIdKey] = id
            });
}
