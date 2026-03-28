namespace ProductLens.Api.AcceptanceTests.TestSupport;

public class TestApiFactory(string connectionString) : WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.UseSetting("ConnectionStrings:ProductLensDb", connectionString);
}
