using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Api.Models;
using ProductLens.Application.UseCases.Products.Commands.ChangeProductPrice;

namespace ProductLens.Api.Endpoints.Products;

public class ChangeProductPriceEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}/price", HandleAsync)
            .WithName("ChangeProductPrice")
            .WithSummary("Change product price")
            .WithDescription("Updates the product price.")
            .WithTags("Products")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        ChangeProductPriceRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new ChangeProductPriceCommand(
            id,
            request.Amount,
            request.Currency);

        var outcome = await mediator.Send(command, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.NoContent();
    }
}
