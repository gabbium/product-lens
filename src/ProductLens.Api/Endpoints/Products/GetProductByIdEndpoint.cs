using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Application.Models;
using ProductLens.Application.UseCases.Products.Queries.GetProductById;

namespace ProductLens.Api.Endpoints.Products;

public class GetProductByIdEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", HandleAsync)
            .WithName("GetProductById")
            .WithSummary("Get product by id")
            .WithDescription("Returns the product details.")
            .WithTags("Products")
            .Produces<ProductDetailsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        var outcome = await mediator.Send(query, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.Ok(outcome.Value);
    }
}
