using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.UpdateProductDetails;

public class UpdateProductDetailsCommandValidator : AbstractValidator<UpdateProductDetailsCommand>
{
    public UpdateProductDetailsCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithState(_ => ProductErrors.IdRequired());

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithState(_ => ProductErrors.NameRequired())
            .MaximumLength(200)
                .WithState(_ => ProductErrors.NameTooLong(200));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
                .WithState(_ => ProductErrors.DescriptionTooLong(1000));
    }
}
