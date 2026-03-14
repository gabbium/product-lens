var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithHostPort(5432);

var productLensDb = postgres.AddDatabase("productlensdb");

builder.AddProject<Projects.ProductLens_Api>("productlensapi")
    .WithReference(productLensDb).WaitFor(productLensDb);

var app = builder.Build();

await app.RunAsync();

