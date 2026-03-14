using ProductLens.Api.Infrastructure.MinimalApi;
using ProductLens.Api.Models;
using ProductLens.Application.UseCases.Products.Commands.UpdateProductDetails;

namespace ProductLens.Api.Endpoints.Products;

public class UpdateProductEndpoint : IEndpoint
{
    public ApiVersion Version => new(1);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", HandleAsync)
            .WithName("UpdateProduct")
            .WithSummary("Update product")
            .WithDescription("Updates product details.")
            .WithTags("Products")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        UpdateProductRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductDetailsCommand(
            id,
            request.Name,
            request.Description);

        var outcome = await mediator.Send(command, cancellationToken);

        return outcome.IsError
            ? CustomResults.Problem(outcome)
            : Results.NoContent();
    }
}
