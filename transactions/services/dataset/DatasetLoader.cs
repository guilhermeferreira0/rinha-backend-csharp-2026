using rinha_backend_csharp_2026.transactions.models;
using rinha_backend_csharp_2026.transactions.services.dataset;
using System.Collections.Frozen;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rinha_backend_csharp_2026.transactions.services.Dataset
{
    public class DatasetLoader
    {
        private const int MAX_JSON_DATA = 3_000_000;

        public async Task<DatasetStore> LoadReference(string path)
        {
            using var file = File.OpenRead(path);
            using var gzip = new GZipStream(file, CompressionMode.Decompress);

            var vectors = new Vector14[MAX_JSON_DATA];
            var labels = new byte[MAX_JSON_DATA];

            var i = 0;
            await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable(
                gzip,
                AppJsonSerializerContext.Default.ReferenceDatasetItem
            ))
            {
                if (item is null)
                    continue;

                vectors[i] = new Vector14
                {
                    V0 = item.Vector[0],
                    V1 = item.Vector[1],
                    V2 = item.Vector[2],
                    V3 = item.Vector[3],
                    V4 = item.Vector[4],
                    V5 = item.Vector[5],
                    V6 = item.Vector[6],
                    V7 = item.Vector[7],
                    V8 = item.Vector[8],
                    V9 = item.Vector[9],
                    V10 = item.Vector[10],
                    V11 = item.Vector[11],
                    V12 = item.Vector[12],
                    V13 = item.Vector[13]
                };

                labels[i] = item.Label == "fraud" ? (byte)1 : (byte)0;
                i++;
            };

            return new DatasetStore(vectors, labels, i);
        }

        public MccRiskTable LoadMccRisk(string path)
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
