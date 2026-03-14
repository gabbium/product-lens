using ProductLens.Api.Extensions;

namespace ProductLens.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioningAndExplorer();
        services.AddOpenApiWithTransformers("v1");
        services.AddSpaCorsPolicy(configuration);
        services.AddExceptionHandling();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseExceptionHandling();
        app.UseSpaCorsPolicy();
        app.MapEndpoints();
        app.MapOpenApiAndScalar();

        return app;
    }
}

