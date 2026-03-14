namespace ProductLens.Application.UseCases.Products.Commands.DiscontinueProduct;

public record DiscontinueProductCommand(Guid ProductId) : ICommand<Outcome>;
