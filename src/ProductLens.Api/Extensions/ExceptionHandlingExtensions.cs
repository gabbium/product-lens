using ProductLens.Api.Infrastructure.ExceptionHandling;

namespace ProductLens.Api.Extensions;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<DefaultExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static WebApplication UseExceptionHandling(this WebApplication app)
    {
        app.UseExceptionHandler();

        return app;
    }
}
