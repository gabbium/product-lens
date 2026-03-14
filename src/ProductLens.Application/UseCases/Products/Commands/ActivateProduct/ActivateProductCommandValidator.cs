using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.ActivateProduct;

public class ActivateProductCommandValidator : AbstractValidator<ActivateProductCommand>
{
    public ActivateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithState(_ => ProductErrors.IdRequired());
    }
}
