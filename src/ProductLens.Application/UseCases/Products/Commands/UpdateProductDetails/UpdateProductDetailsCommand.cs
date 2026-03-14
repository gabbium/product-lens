namespace ProductLens.Application.UseCases.Products.Commands.UpdateProductDetails;

public sealed record UpdateProductDetailsCommand(
    Guid ProductId,
    string Name,
    string? Description)
    : ICommand<Outcome>;
