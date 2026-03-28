using ProductLens.Infrastructure.Data;

namespace ProductLens.Api.AcceptanceTests.TestSupport;

public class PostgresContainer
{
    private readonly PostgreSqlContainer _container =
        new PostgreSqlBuilder("postgres:16")
            .Build();

    private Respawner _respawner = default!;

    public string ConnectionString => _container.GetConnectionString();

    public Task InitializeAsync() => _container.StartAsync();

    public async Task InitializeDatabaseAsync(IServiceProvider services)
    {
        if (_respawner != null)
        {
            return;
        }

        using var scope = services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initialiser.InitializeAsync();

        await InitializeRespawn();
    }

    private async Task InitializeRespawn()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }

    public async Task ResetAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();

        await _respawner!.ResetAsync(connection);
    }

    public ValueTask DisposeAsync() => _container.DisposeAsync();
}
