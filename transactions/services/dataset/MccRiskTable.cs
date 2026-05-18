using System.Collections.Frozen;
using System.Text.Json;

namespace rinha_backend_csharp_2026.transactions.services.dataset
{
    public class MccRiskTable(FrozenDictionary<string, float> table)
    {
        private readonly FrozenDictionary<string, float> _table = table;
        public const float Default = 0.5f;

        public float Get(string mcc) => _table.TryGetValue(mcc, out var v) ? v : Default;
    }
}
