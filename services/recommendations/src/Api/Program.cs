using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using RecommendCoffee.Recommendations.Api;
using RecommendCoffee.Recommendations.Application.Common;
using RecommendCoffee.Recommendations.Application.EventHandlers;
using RecommendCoffee.Recommendations.Domain.Aggregates.CustomerAggregate;
using RecommendCoffee.Recommendations.Domain.Aggregates.ProductAggregate;
using RecommendCoffee.Recommendations.Infrastructure.EventBus;
using RecommendCoffee.Recommendations.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultDatabase"), 
        opts => opts.EnableRetryOnFailure());
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddDapr(daprClientBuilder =>
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    
        serializerOptions.Converters.Add(new JsonStringEnumConverter());    
    });

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase"))
    .AddDbContextCheck<ApplicationDbContext>();

builder.AddTelemetry("Recommendations",
    "RecommendCoffee.Recommendations.Api",
    "RecommendCoffee.Recommendations.Application",
    "RecommendCoffee.Recommendations.Domain",
    "RecommendCoffee.Recommendations.Infrastructure");

builder.Services.AddSingleton<IEventPublisher, DaprEventPublisher>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CustomerRegisteredEventHandler>();
builder.Services.AddScoped<ProductRegisteredEventHandler>();
builder.Services.AddScoped<ProductUpdatedEventHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await dbContext.Database.MigrateAsync();    
}

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseCloudEvents();

app.MapHealthChecks("/healthz", new HealthCheckOptions 
{
    AllowCachingResponses = false
});

app.MapSubscribeHandler();
app.MapControllers();

app.Run();
