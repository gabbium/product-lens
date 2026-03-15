using ProductLens.Application.Interfaces;
using ProductLens.Application.Models;
using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.UseCases.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateProductCommand, Outcome<ProductDetailsResponse>>
{
    public async ValueTask<Outcome<ProductDetailsResponse>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var price = new Money(command.Amount, command.Currency);

        var product = new Product(
            command.Name,
            price,
            command.Description);

        if (product.Price.Currency == "BRL")
        {
            return ProductErrors.DiscontinueNotAllowedForDraft(product.Id);
        }

        await context.Products.AddAsync(product, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        var response = new ProductDetailsResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Amount = product.Price.Amount,
            Currency = product.Price.Currency,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            LastModifiedAt = product.LastModifiedAt
        };

        return response;
    }
}

