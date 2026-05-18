using rinha_backend_csharp_2026.transactions.models;
using rinha_backend_csharp_2026.transactions.services;
using rinha_backend_csharp_2026.transactions.services.Dataset;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var resourcesPath = Environment.GetEnvironmentVariable("RESOURCES_PATH")
    ?? Path.Combine(builder.Environment.ContentRootPath, "resources");

var mccRiskTablePath = Path.Combine(resourcesPath, "mcc_risk.json");
var referencesPath = Path.Combine(resourcesPath, "references.json.gz");

var datasetLoader = new DatasetLoader();
var datasetStore = await datasetLoader.LoadReference(referencesPath);
var mccRiskTable = datasetLoader.LoadMccRisk(mccRiskTablePath);

builder.Services.AddSingleton(mccRiskTable);
builder.Services.AddSingleton(datasetStore);
builder.Services.AddScoped<VectorBuilder>();
builder.Services.AddScoped<VectorSearch>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

app.MapGet("/ready", () => Results.Ok());

app.MapPost("/fraud-score", (
    TransactionRequest req,
    TransactionService service,
    CancellationToken ct = default) =>
{
    var result = service.Process(req, ct);
    return Results.Ok(result);
});

await app.RunAsync();

[JsonSerializable(typeof(Transaction))]
[JsonSerializable(typeof(List<Transaction>))]
[JsonSerializable(typeof(TransactionRequest))]
[JsonSerializable(typeof(TransactionResponse))]
[JsonSerializable(typeof(List<ReferenceDatasetItem>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
