using System.Text.Json.Serialization;

namespace rinha_backend_csharp_2026.transactions.models
{
    public class TransactionRequest
    {
        public required string Id { get; set; }
        public required Transaction Transaction { get; set; }
        public required Customer Customer { get; set; }
        public required Merchant Merchant { get; set; }
        public required Terminal Terminal { get; set; }

        [JsonPropertyName("last_transaction")]
        public LastTransaction? LastTransaction { get; set; } 
    }

    public class Transaction
    {
        public float Amount { get; set; }
        public int Installments { get; set; }

        [JsonPropertyName("requested_at")]
        public required DateTime RequestedAt { get; set; }
    }

    public class Customer
    {
        [JsonPropertyName("avg_amount")]
        public float AvgMount { get; set; }

        [JsonPropertyName("tx_count_24h")]
        public int TxCount24h { get; set; }

        [JsonPropertyName("known_merchants")]
        public string[] KnownMerchants { get; set; } = [];
    }

    public class Merchant
    {
        public required string Id { get; set; } = string.Empty;
        public required string Mcc { get; set; } = string.Empty;

        [JsonPropertyName("avg_amount")]
        public float AvgAmount { get; set; }
    }

    public class Terminal
    {
        [JsonPropertyName("is_online")]
        public bool IsOnline { get; set; }

        [JsonPropertyName("card_present")]
        public bool CardPresent { get; set; }

        [JsonPropertyName("km_from_home")]
        public float KmFromHome { get; set; }
    }

    public class LastTransaction
    {
        public required DateTime Timestamp { get; set; }

        [JsonPropertyName("km_from_current")]
        public float KmFromCurrent { get; set; }
    }

    public class TransactionResponse
    {
        public bool Approved { get; init; }
        public float FraudScore { get; init; }
    }
}
