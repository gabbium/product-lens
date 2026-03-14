using ProductLens.Application.Models;

namespace ProductLens.Application.UseCases.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Amount,
    string Currency)
    : ICommand<Outcome<ProductDetailsResponse>>;
