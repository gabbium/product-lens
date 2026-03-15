using ProductLens.Api.Models;

namespace ProductLens.Api.AcceptanceTests.Steps.Products;

[Binding]
public class ProductActionSteps(ScenarioContext context)
{
    private readonly HttpClient _client = context.Get<HttpClient>();

    [When("I get the product by id")]
    public async Task WhenIGetProduct()
    {
        var id = context.Get<Guid>();

        var response = await _client.GetAsync($"/api/v1/products/{id}");

        context.Set(response);
    }

    [When("I create the product")]
    public async Task WhenICreateProduct()
    {
        var request = context.Get<CreateProductRequest>();

        var response = await _client.PostAsJsonAsync("/api/v1/products", request);

        context.Set(response);
    }

    [When("I change the product price to {decimal} {string}")]
    public async Task WhenIChangeProductPrice(decimal amount, string currency)
    {
        var id = context.Get<Guid>();

        var request = new ChangeProductPriceRequest
        {
            Amount = amount,
            Currency = currency
        };

        var response = await _client.PutAsJsonAsync($"/api/v1/products/{id}/price", request);

        context.Set(response);
    }

    [When("I update the product details to {string} {string}")]
    public async Task WhenIUpdateProductDetails(string name, string description)
    {
        var id = context.Get<Guid>();

        var request = new UpdateProductRequest
        {
            Name = name,
            Description = description
        };

        var response = await _client.PutAsJsonAsync($"/api/v1/products/{id}", request);

        context.Set(response);
    }

    [When("I activate the product")]
    public async Task WhenIActivateProduct()
    {
        var id = context.Get<Guid>();

        var response = await _client.PutAsync($"/api/v1/products/{id}/activate", null);

        context.Set(response);
    }

    [When("I discontinue the product")]
    public async Task WhenIDiscontinueProduct()
    {
        var id = context.Get<Guid>();

        var response = await _client.PutAsync($"/api/v1/products/{id}/discontinue", null);

        context.Set(response);
    }

    [When("I delete the product")]
    public async Task WhenIDeleteProduct()
    {
        var id = context.Get<Guid>();

        var response = await _client.DeleteAsync($"/api/v1/products/{id}");

        context.Set(response);
    }
}
