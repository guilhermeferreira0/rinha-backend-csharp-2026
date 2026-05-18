using System.Text.Json.Serialization;

namespace rinha_backend_csharp_2026.transactions.models
{
    public sealed class ReferenceDatasetItem
    {
        [JsonPropertyName("vector")]
        public required Half[] Vector { get; init; }

        [JsonPropertyName("label")]
        public required string Label { get; init; }
    }
}
