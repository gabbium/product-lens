using ProductLens.Application.Errors;

namespace ProductLens.Application.UseCases.Products.Queries.ListProducts;

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
                .WithState(_ => PaginationErrors.PageNumberInvalid(1));

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
                .WithState(_ => PaginationErrors.PageNumberInvalid(1));
    }
}
