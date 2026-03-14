using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteProductCommand, Outcome>
{
    public async ValueTask<Outcome> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return Outcome.Success();
        }

        if (product.Status != ProductStatus.Draft)
        {
            return ProductErrors.DeleteNotAllowedForNonDraft(command.ProductId);
        }

        context.Products.Remove(product);

        await context.SaveChangesAsync(cancellationToken);

        return Outcome.Success();
    }
}
