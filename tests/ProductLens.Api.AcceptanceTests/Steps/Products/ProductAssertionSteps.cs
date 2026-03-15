using ProductLens.Application.Models;

namespace ProductLens.Api.AcceptanceTests.Steps.Products;

[Binding]
public class ProductAssertionSteps(ScenarioContext context)
{
    [Then("the product should be created")]
    public void ThenProductCreated()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Then("the created product details should be returned")]
    public async Task ThenCreatedProductReturned()
    {
        var response = context.Get<HttpResponseMessage>();

        var product = await response.Content
            .ReadFromJsonAsync<ProductDetailsResponse>();

        product.ShouldNotBeNull();
        product.Id.ShouldNotBe(Guid.Empty);
    }

    [Then("the product details should be returned")]
    public async Task ThenProductReturned()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Then("the product price should be changed")]
    public void ThenProductPriceChanged()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Then("the product should be updated")]
    public void ThenProductUpdated()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Then("the product should be activated")]
    public void ThenProductActivated()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Then("the product should be discontinued")]
    public void ThenProductDiscontinued()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Then("the product should be deleted")]
    public void ThenProductDeleted()
    {
        var response = context.Get<HttpResponseMessage>();

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}
