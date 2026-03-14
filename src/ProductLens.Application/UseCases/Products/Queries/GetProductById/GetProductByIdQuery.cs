using ProductLens.Application.Models;

namespace ProductLens.Application.UseCases.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId)
    : IQuery<Outcome<ProductDetailsResponse>>;
