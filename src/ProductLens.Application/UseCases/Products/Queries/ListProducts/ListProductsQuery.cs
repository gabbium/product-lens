using ProductLens.Application.Models;

namespace ProductLens.Application.UseCases.Products.Queries.ListProducts;

public record ListProductsQuery(int PageNumber, int PageSize)
    : IQuery<Outcome<PagedList<ProductListItemResponse>>>;
