using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Application.UseCases.Products.Commands.DeleteProduct;

namespace ProductLens.Api.Endpoints.Products;

public class DeleteProductEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", HandleAsync)
            .WithName("DeleteProduct")
            .WithSummary("Delete product")
            .WithDescription("Deletes a product.")
            .WithTags("Products")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);

        var outcome = await mediator.Send(command, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.NoContent();
    }
}
