namespace ProductLens.Api.AcceptanceTests.TestSupport;

[Binding]
public class TestHooks(ScenarioContext scenarioContext)
{
    private static readonly PostgresContainer _database = new();
    private TestApiFactory _factory = default!;

    [BeforeTestRun]
    public static Task BeforeTestRun() => _database.InitializeAsync();

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _factory = new TestApiFactory(_database.ConnectionString);
        await _database.InitializeDatabaseAsync(_factory.Services);

        var client = _factory.CreateClient();

        scenarioContext.Set(client);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        _factory.Dispose();
        await _database.ResetAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun() => await _database.DisposeAsync();
}
