using rinha_backend_csharp_2026.transactions;
using rinha_backend_csharp_2026.transactions.models;
using rinha_backend_csharp_2026.transactions.services;
using rinha_backend_csharp_2026.transactions.services.Dataset;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var mccRiskTablePath = Path.Combine(Directory.GetCurrentDirectory(), "resources", "mcc_risk.json");
var mccRiskTable = MccRiskTable.Load(mccRiskTablePath);

var referencesPath = Path.Combine(Directory.GetCurrentDirectory(),"resources", "references.json.gz");
var datasetLoader = new DatasetLoader();
var datasetStore = datasetLoader.Load(referencesPath);

builder.Services.AddSingleton(mccRiskTable);
builder.Services.AddSingleton(datasetStore);
builder.Services.AddScoped<VectorBuilder>();
builder.Services.AddScoped<KnnSearchService>();
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