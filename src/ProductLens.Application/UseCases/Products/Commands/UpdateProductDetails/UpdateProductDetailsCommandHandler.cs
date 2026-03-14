using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.UpdateProductDetails;

public class UpdateProductDetailsCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateProductDetailsCommand, Outcome>
{
    public async ValueTask<Outcome> Handle(UpdateProductDetailsCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return ProductErrors.NotFound(command.ProductId);
        }

        product.UpdateDetails(command.Name, command.Description);

        await context.SaveChangesAsync(cancellationToken);

        return Outcome.Success();
    }
}
