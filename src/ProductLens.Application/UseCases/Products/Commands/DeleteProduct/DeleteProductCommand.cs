namespace ProductLens.Application.UseCases.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<Outcome>;
