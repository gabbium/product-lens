using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Api.Models;
using ProductLens.Application.Models;
using ProductLens.Application.UseCases.Products.Commands.CreateProduct;

namespace ProductLens.Api.Endpoints.Products;

public class CreateProductEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", HandleAsync)
            .WithName("CreateProduct")
            .WithSummary("Create product")
            .WithDescription("Creates a new product.")
            .WithTags("Products")
            .Produces<ProductDetailsResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> HandleAsync(
        CreateProductRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Amount,
            request.Currency);

        var outcome = await mediator.Send(command, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.CreatedAtRoute(
                "GetProductById",
                new { id = outcome.Value.Id },
                outcome.Value);
    }
}
