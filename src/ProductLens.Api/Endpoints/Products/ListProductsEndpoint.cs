using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Api.Models;
using ProductLens.Application.Models;
using ProductLens.Application.UseCases.Products.Queries.ListProducts;

namespace ProductLens.Api.Endpoints.Products;

public class ListProductsEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", HandleAsync)
            .WithName("ListProducts")
            .WithSummary("List products")
            .WithDescription("Returns a paginated list of products.")
            .WithTags("Products")
            .Produces<PagedList<ProductListItemResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] ListProductsRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new ListProductsQuery(request.PageNumber, request.PageSize);

        var outcome = await mediator.Send(query, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.Ok(outcome.Value);
    }
}
