using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.DiscontinueProduct;

public class DiscontinueProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DiscontinueProductCommand, Outcome>
{
    public async ValueTask<Outcome> Handle(
        DiscontinueProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return ProductErrors.NotFound(command.ProductId);
        }

        product.Discontinue();

        await context.SaveChangesAsync(cancellationToken);

        return Outcome.Success();
    }
}
