using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Application.UseCases.Products.Commands.DiscontinueProduct;

namespace ProductLens.Api.Endpoints.Products;

public class DiscontinueProductEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}/discontinue", HandleAsync)
            .WithName("DiscontinueProduct")
            .WithSummary("Discontinue product")
            .WithDescription("Marks product as discontinued.")
            .WithTags("Products")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new DiscontinueProductCommand(id);

        var outcome = await mediator.Send(command, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.NoContent();
    }
}
