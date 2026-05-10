using System.Collections.Frozen;
using System.Text.Json;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class MccRiskTable(FrozenDictionary<string, float> table)
    {
        private readonly FrozenDictionary<string, float> _table = table;
        public const float Default = 0.5f;

        public float Get(string mcc) => _table.TryGetValue(mcc, out var v) ? v : Default;

        public static MccRiskTable Load(string path)
        {
            using var fs = File.OpenRead(path);
            using var doc = JsonDocument.Parse(fs);

            var dict = new Dictionary<string, float>();
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                dict[prop.Name] = prop.Value.GetSingle();
            }

            return new MccRiskTable(dict.ToFrozenDictionary());
        }
    }
}
