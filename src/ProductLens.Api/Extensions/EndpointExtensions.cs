using ProductLens.Api.Infrastructure.MinimalApi;

namespace ProductLens.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointTypes = typeof(Program).Assembly
            .DefinedTypes
            .Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                typeof(IEndpoint).IsAssignableFrom(t));

        var endpoints = endpointTypes
            .Select(t => (IEndpoint)Activator.CreateInstance(t)!);

        var endpointsByVersion = endpoints
            .GroupBy(e => e.Version);

        foreach (var group in endpointsByVersion)
        {
            var version = group.Key;

            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(version)
                .ReportApiVersions()
                .Build();

            var routeGroup = app
                .MapGroup($"/api/v{version}")
                .WithApiVersionSet(versionSet)
                .HasApiVersion(version);

            foreach (var endpoint in group)
            {
                endpoint.MapEndpoint(routeGroup);
            }
        }
    }
}
