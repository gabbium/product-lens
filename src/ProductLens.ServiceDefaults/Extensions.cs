using ProductLens.ServiceDefaults.HealthCheck;
using ProductLens.ServiceDefaults.OpenTelemetry;
using ProductLens.ServiceDefaults.SerilogLogging;

namespace ProductLens.ServiceDefaults;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.AddDefaultSerilog();

        builder.AddDefaultOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        app.UseDefaultSerilog();

        app.MapDefaultHealthChecks();

        return app;
    }
}
