using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithState(_ => ProductErrors.IdRequired());
    }
}
