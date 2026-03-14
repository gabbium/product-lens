namespace ProductLens.Api.Infrastructure.MinimalApi;

public interface IEndpoint
{
    ApiVersion Version { get; }

    void MapEndpoint(IEndpointRouteBuilder app);
}
