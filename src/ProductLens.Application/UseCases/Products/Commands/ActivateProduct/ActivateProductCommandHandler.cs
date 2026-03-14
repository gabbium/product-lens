using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.ActivateProduct;

public class ActivateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ActivateProductCommand, Outcome>
{
    public async ValueTask<Outcome> Handle(
        ActivateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return ProductErrors.NotFound(command.ProductId);
        }

        product.Activate();

        await context.SaveChangesAsync(cancellationToken);

        return Outcome.Success();
    }
}
