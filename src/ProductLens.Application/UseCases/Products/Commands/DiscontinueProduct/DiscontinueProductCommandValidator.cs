using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.DiscontinueProduct;

public class DiscontinueProductCommandValidator : AbstractValidator<DiscontinueProductCommand>
{
    public DiscontinueProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithState(_ => ProductErrors.IdRequired());
    }
}
