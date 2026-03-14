using ProductLens.Application.Interfaces;
using ProductLens.Application.Models;

namespace ProductLens.Application.UseCases.Products.Queries.ListProducts;

public class ListProductsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<ListProductsQuery, Outcome<PagedList<ProductListItemResponse>>>
{
    public async ValueTask<Outcome<PagedList<ProductListItemResponse>>> Handle(
        ListProductsQuery query,
        CancellationToken cancellationToken)
    {
        var baseQuery = context.Products.AsNoTracking();

        var totalItems = await baseQuery.CountAsync(cancellationToken);

        var items = await baseQuery
            .OrderByDescending(p => p.CreatedAt)
            .ThenBy(p => p.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductListItemResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Amount = p.Price.Amount,
                Currency = p.Price.Currency,
                Status = p.Status
            })
            .ToListAsync(cancellationToken);

        return new PagedList<ProductListItemResponse>(
            items,
            totalItems,
            query.PageNumber,
            query.PageSize);
    }
}
