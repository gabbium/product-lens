using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;
using ProductLens.Infrastructure.Data.Configurations;

namespace ProductLens.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ProductStatus>();
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
    }
}
