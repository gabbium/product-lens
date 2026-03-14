using ProductLens.Application.Interfaces;
using ProductLens.Domain.AggregatesModel.ProductAggregate;
using ProductLens.Infrastructure.Data;
using ProductLens.Infrastructure.Data.Interceptors;

namespace ProductLens.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProductLensDb");

        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(connectionString, o =>
            {
                o.MapEnum<ProductStatus>();
            });
        });

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
