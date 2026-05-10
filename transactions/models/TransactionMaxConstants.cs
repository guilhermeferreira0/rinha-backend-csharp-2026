namespace rinha_backend_csharp_2026.transactions.models
{
    public abstract class TransactionMaxConstants
    {
        public const float MaxAmount = 10_000f;
        public const float MaxInstallments = 12f;
        public const float AmountVsAvgRatio = 10f;
        public const float MaxMinutes = 1_440f;
        public const float MaxKm = 1_000f;
        public const float MaxTxCount24h = 20f;
        public const float MaxMerchantAvgAmount = 10_000f;
        public const float SentinelMissing = -1f;
    }
}
