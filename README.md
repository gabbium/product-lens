# ProductLens

![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gabbium_product-lens?server=https%3A%2F%2Fsonarcloud.io)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gabbium_product-lens?server=https%3A%2F%2Fsonarcloud.io)

ProductLens is a lightweight **product catalog API for .NET** built with
**Clean Architecture**, **CQRS**, **Minimal APIs**, and **PostgreSQL**.

It provides a focused backend for managing products through their lifecycle:

- create products
- list and retrieve products
- update product details
- change product prices
- activate products
- discontinue products
- delete draft products

The solution is designed to keep business rules explicit in the domain,
application flows isolated in use cases, and HTTP concerns thin at the API
boundary.

It also uses **OutcomeCore** for explicit success/error results instead of
relying on exceptions for normal control flow.

---

# Tech Stack

ProductLens is built with:

- **.NET 10**
- **ASP.NET Core Minimal APIs**
- **Mediator** for CQRS-style request handling
- **FluentValidation** for input validation
- **Entity Framework Core**
- **PostgreSQL**
- **.NET Aspire** for local orchestration
- **Scalar** for API documentation
- **xUnit**, **Shouldly**, **Reqnroll**, and **Testcontainers** for testing

---

# What The Project Does

The API manages products identified by:

- name
- optional description
- price amount
- ISO currency code
- lifecycle status

Each product moves through a simple lifecycle:

```text
Draft -> Active -> Discontinued
```

Business rules are enforced in the domain and application layers. For example:

- a product starts as `Draft`
- only `Draft` products can be deleted
- a `Draft` product cannot be discontinued
- a `Discontinued` product cannot be modified
- product price must be greater than zero
- currency must be a 3-character ISO code

---

# Architecture

The solution is split into clear layers:

```text
ProductLens.Api
   ->
ProductLens.Application
   ->
ProductLens.Domain
   ->
ProductLens.Infrastructure
```

### ProductLens.Api

Exposes versioned **Minimal API** endpoints under:

```text
/api/v1
```

It is responsible for:

- routing
- request/response models
- OpenAPI and Scalar UI
- exception handling
- mapping `Outcome` failures to HTTP responses

### ProductLens.Application

Contains the use cases, validators, pipeline behaviors, and response models.

This layer follows a CQRS-style structure with:

- commands for write operations
- queries for read operations
- `Mediator` handlers
- logging and validation behaviors

### ProductLens.Domain

Contains the core business model, including:

- `Product`
- `Money`
- `ProductStatus`
- domain-specific errors
- domain exceptions for rule violations

### ProductLens.Infrastructure

Provides persistence and environment integration:

- EF Core `DbContext`
- PostgreSQL mapping
- entity configurations
- migrations
- database initialization and seed data

### ProductLens.AppHost

Uses **.NET Aspire** to orchestrate the API and a PostgreSQL instance for local
development.

---

# Core Domain Model

The main aggregate is `Product`.

```csharp
public class Product : AuditableEntity, IAggregateRoot
{
    public string Name { get; }
    public string? Description { get; }
    public Money Price { get; }
    public ProductStatus Status { get; }
}
```

Product status is represented by:

```csharp
public enum ProductStatus
{
    Draft,
    Active,
    Discontinued
}
```

Money is modeled as a value object-like type:

```csharp
public class Money(decimal amount, string currency)
{
    public decimal Amount { get; }
    public string Currency { get; }
}
```

---

# API Endpoints

ProductLens exposes the following endpoints in version `v1`:

```text
POST   /api/v1/products
GET    /api/v1/products
GET    /api/v1/products/{id}
PUT    /api/v1/products/{id}
PUT    /api/v1/products/{id}/price
PUT    /api/v1/products/{id}/activate
PUT    /api/v1/products/{id}/discontinue
DELETE /api/v1/products/{id}
```

### Example Create Request

```json
{
  "name": "Smartphone Pro Max",
  "description": "Latest model",
  "amount": 1200.0,
  "currency": "USD"
}
```

### Example List Response

Listing products returns a paginated payload:

```json
{
  "items": [
    {
      "id": "9ccf6f40-c68e-4a88-b8c2-a0f5476f9328",
      "name": "Smartphone Pro Max",
      "amount": 1200.0,
      "currency": "USD",
      "status": "Draft"
    }
  ],
  "pageNumber": 1,
  "pageSize": 20,
  "totalItems": 1,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

---

# Request Flow

Each request follows a predictable application flow:

```text
HTTP Endpoint
   ->
Mediator
   ->
ValidationBehavior
   ->
Handler
   ->
Outcome / Domain Exception
   ->
HTTP Response
```

Validation failures return `400 Bad Request`.

Business rule violations return `422 Unprocessable Entity`.

Missing resources return `404 Not Found`.

---

# Validation And Errors

Input validation is implemented with **FluentValidation**.

Current validation rules include:

- product name is required
- product name maximum length is 200
- description maximum length is 1000
- amount must be greater than zero
- currency is required
- currency must contain exactly 3 characters
- pagination page number must be at least 1
- pagination page size must be between 1 and 100

Business and not-found errors are represented explicitly with `OutcomeCore`
errors such as:

```csharp
return ProductErrors.NotFound(productId);
```

```csharp
return ProductErrors.DeleteNotAllowedForNonDraft(productId);
```

The API maps those errors into `ProblemDetails` responses.

---

# Local Development

The easiest way to run the project locally is through the Aspire app host.

Requirements:

- **.NET 10 SDK**
- **Docker** running locally

Start the full stack with:

```bash
dotnet run --project src/ProductLens.AppHost
```

This boots:

- the `ProductLens.Api`
- a PostgreSQL container used by the application

In development, the API also:

- applies EF Core migrations automatically
- seeds sample products when the database is empty

The root path redirects to the Scalar API reference:

```text
/scalar/v1
```

---

# Running The API Without Aspire

You can also run the API directly, as long as the connection string is
provided:

```bash
dotnet run --project src/ProductLens.Api
```

Set:

```text
ConnectionStrings__ProductLensDb
```

to a valid PostgreSQL connection string before starting the API this way.

---

# Testing

The solution includes multiple test layers:

- **Domain unit tests** for aggregate behavior
- **Acceptance tests** for API scenarios using Reqnroll
- **Integration-oriented infrastructure coverage**

Run all tests with:

```bash
dotnet test
```

Acceptance tests use **Testcontainers** with PostgreSQL, so Docker is expected
to be available when executing the full test suite.

---

# Example Use Cases

Typical flows supported by the API include:

1. Create a product in `Draft` status.
2. Activate the product when it becomes available.
3. Update details or price while the product is active.
4. Discontinue the product when it should no longer be sold.
5. Prevent edits after discontinuation.

---

# License

This project is licensed under the MIT License. See the
[LICENSE](LICENSE) file for details.
