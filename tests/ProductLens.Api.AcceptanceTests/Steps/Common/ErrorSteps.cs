namespace ProductLens.Api.AcceptanceTests.Steps.Common;

[Binding]
public class ErrorSteps(ScenarioContext context)
{
    [Then("a not found error should occur with message {string}")]
    public async Task ThenNotFound(string message)
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        problem!.Detail.ShouldBe(message);
    }

    [Then("a business rule violation should occur with message {string}")]
    public async Task ThenBusinessRule(string message)
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        problem!.Detail.ShouldBe(message);
    }
}
