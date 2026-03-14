using ProductLens.Application.Behaviors;
using ProductLens.Application.UseCases.Products.Queries.ListProducts;

namespace ProductLens.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Assemblies =
            [
                typeof(ListProductsQuery).Assembly
            ];
            options.PipelineBehaviors =
            [
                typeof(LoggingBehavior<,>),
                typeof(ValidationBehavior<,>)
            ];
        });

        services.AddValidatorsFromAssemblyContaining<ListProductsQuery>();

        return services;
    }
}
