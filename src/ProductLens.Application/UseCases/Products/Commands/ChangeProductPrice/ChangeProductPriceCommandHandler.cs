using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.ChangeProductPrice;

public class ChangeProductPriceCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ChangeProductPriceCommand, Outcome>
{
    public async ValueTask<Outcome> Handle(
        ChangeProductPriceCommand command,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return ProductErrors.NotFound(command.ProductId);
        }

        var price = new Money(command.Amount, command.Currency);

        product.ChangePrice(price);

        await context.SaveChangesAsync(cancellationToken);

        return Outcome.Success();
    }
}
