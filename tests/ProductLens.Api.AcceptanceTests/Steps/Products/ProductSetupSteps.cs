using ProductLens.Api.Models;
using ProductLens.Application.Models;

namespace ProductLens.Api.AcceptanceTests.Steps.Products;

[Binding]
public class ProductSetupSteps(ScenarioContext context)
{
    private readonly HttpClient _client = context.Get<HttpClient>();

    [Given("the following product data")]
    public void GivenTheFollowingProductData(Table table)
    {
        var row = table.Rows[0];

        var request = new CreateProductRequest
        {
            Name = row["Name"],
            Description = row["Description"],
            Amount = decimal.Parse(row["Amount"]),
            Currency = row["Currency"]
        };

        context.Set(request);
    }

    [Given("a product exists")]
    public async Task GivenAProductExists()
    {
        var request = new CreateProductRequest
        {
            Name = "Test Product",
            Description = "Test",
            Amount = 100,
            Currency = "USD"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/products", request);

        var product = await response.Content.ReadFromJsonAsync<ProductDetailsResponse>();

        context.Set(product!.Id);
    }

    [Given("the product status is {string}")]
    public async Task GivenTheProductStatusIs(string status)
    {
        var id = context.Get<Guid>();

        if (status == "Active")
        {
            await _client.PutAsync($"/api/v1/products/{id}/activate", null);
        }

        if (status == "Discontinued")
        {
            await _client.PutAsync($"/api/v1/products/{id}/activate", null);
            await _client.PutAsync($"/api/v1/products/{id}/discontinue", null);
        }
    }

    [Given("a non existing product id")]
    public void GivenNonExistingId()
        => context.Set(Guid.NewGuid());

    [Given("an empty product id")]
    public void GivenEmptyId()
        => context.Set(Guid.Empty);
}
