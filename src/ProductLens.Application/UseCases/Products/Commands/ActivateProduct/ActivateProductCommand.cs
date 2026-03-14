namespace ProductLens.Application.UseCases.Products.Commands.ActivateProduct;

public record ActivateProductCommand(Guid ProductId) : ICommand<Outcome>;
