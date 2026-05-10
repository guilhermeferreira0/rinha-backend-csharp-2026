using rinha_backend_csharp_2026.transactions.models;
using System.IO.Compression;
using System.Text.Json;

namespace rinha_backend_csharp_2026.transactions.services.Dataset
{
    public class DatasetLoader
    {
        public DatasetStore Load(string path)
        {
            using var file = File.OpenRead(path);
            using var gzip = new GZipStream(file, CompressionMode.Decompress);

            var items = JsonSerializer.Deserialize<List<ReferenceDatasetItem>>(gzip) 
                ?? throw new InvalidOperationException("Failed to load references dataset.");

            var vectors = new Vector14[items.Count];
            var labels = new byte[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];

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
            }

            return new DatasetStore(vectors, labels);
        }
    }
}
