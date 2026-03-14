using ProductLens.Api;
using ProductLens.Application;
using ProductLens.Infrastructure;
using ProductLens.Infrastructure.Data;
using ProductLens.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.UseServiceDefaults();

app.UseApiServices();

await app.RunAsync();
