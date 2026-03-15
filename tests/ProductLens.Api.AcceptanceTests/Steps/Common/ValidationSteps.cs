namespace ProductLens.Api.AcceptanceTests.Steps.Common;

[Binding]
public class ValidationSteps(ScenarioContext context)
{
    [Then("a validation error should occur for {string}")]
    public async Task ThenValidationError(string field)
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var problem = await response.Content
            .ReadFromJsonAsync<HttpValidationProblemDetails>();

        problem.ShouldNotBeNull();
        problem.Errors.ShouldContainKey(field);
    }
}
