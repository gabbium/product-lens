using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Application.UseCases.Products.Commands.ActivateProduct;

namespace ProductLens.Api.Endpoints.Products;

public class ActivateProductEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}/activate", HandleAsync)
            .WithName("ActivateProduct")
            .WithSummary("Activate product")
            .WithDescription("Activates a product.")
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
        var command = new ActivateProductCommand(id);

        var outcome = await mediator.Send(command, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.NoContent();
    }
}
