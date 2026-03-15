namespace ProductLens.Api.AcceptanceTests.TestSupport;

[Binding]
public class TestHooks(ScenarioContext scenarioContext)
{
    private TestApiFactory _factory = default!;

    [BeforeScenario]
    public void BeforeScenario()
    {
        _factory = new TestApiFactory();

        var client = _factory.CreateClient();

        scenarioContext.Set(client);
    }

    [AfterScenario]
    public void AfterScenario() => _factory.Dispose();
}
