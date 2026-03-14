using ProductLens.Domain.AggregatesModel.ProductAggregate;

namespace ProductLens.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
