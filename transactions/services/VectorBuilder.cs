using rinha_backend_csharp_2026.transactions.models;
using rinha_backend_csharp_2026.transactions.services.dataset;
using System.Collections.Frozen;
using System.Runtime.CompilerServices;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class VectorBuilder(MccRiskTable mccRiskTable)
    {
        private readonly MccRiskTable _mccRiskTable = mccRiskTable;

        public Vector14 Build(in TransactionRequest req)
        {
            var ratio = req.Transaction.Amount / MathF.Max(req.Customer.AvgMount, 1f);

            return new Vector14
            {
                V0 = Clamp01(req.Transaction.Amount / TransactionMaxConstants.MaxAmount),
                V1 = Clamp01(req.Transaction.Installments / TransactionMaxConstants.MaxInstallments),
                V2 = Clamp01(MathF.Log10(ratio + 1f) / TransactionMaxConstants.AmountVsAvgRatio),
                V3 = (Half)(req.Transaction.RequestedAt.Hour / 23),
                V4 = (Half)((((int)req.Transaction.RequestedAt.DayOfWeek + 6) % 7) / 6),
                V5 = NormalizeMinutesSinceLastTransaction(req),
                V6 = NormalizeKmFromLastTransaction(req),
                V7 = Clamp01(req.Terminal.KmFromHome / TransactionMaxConstants.MaxKm),
                V8 = Clamp01(req.Customer.TxCount24h / TransactionMaxConstants.MaxTxCount24h),
                V9 = (Half)(req.Terminal.IsOnline ? 1 : 0),
                V10 = (Half)(req.Terminal.CardPresent ? 1 : 0),
                V11 = (Half)(ContainsMerchant(req.Customer.KnownMerchants, req.Merchant.Id) ? 0 : 1),
                V12 = (Half)_mccRiskTable.Get(req.Merchant.Mcc),
                V13 = Clamp01(req.Merchant.AvgAmount / TransactionMaxConstants.MaxMerchantAvgAmount)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Half Clamp01(float value)
        {
            if (value < 0f) return (Half)0;
            if (value > 1f) return (Half)1;
            return (Half)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Half NormalizeMinutesSinceLastTransaction(in TransactionRequest req)
        {
            if (req.LastTransaction is null)
                return (Half)(-1);

            var minutes = (float)
                (req.Transaction.RequestedAt - req.LastTransaction.Timestamp)
                .TotalMinutes;

            return Clamp01(minutes / TransactionMaxConstants.MaxMinutes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Half NormalizeKmFromLastTransaction(in TransactionRequest req)
        {
            if (req.LastTransaction is null)
                return (Half)(-1);

            return Clamp01(
                (float)req.LastTransaction.KmFromCurrent /
                TransactionMaxConstants.MaxKm);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ContainsMerchant(
        string[] merchants,
        string merchantId)
        {
            for (var i = 0; i < merchants.Length; i++)
            {
                if (merchants[i] == merchantId)
                    return true;
            }

            return false;
        }
    }
}
