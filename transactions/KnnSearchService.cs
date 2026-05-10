using rinha_backend_csharp_2026.transactions.services;
using rinha_backend_csharp_2026.transactions.services.Dataset;
using System.Numerics;
using System.Text.Json;

namespace rinha_backend_csharp_2026.transactions
{
    public class KnnSearchService(DatasetStore datasetStore)
    {
        private readonly DatasetStore _datasetStore = datasetStore;

        public float SearchFraudScore(in Vector14 query)
        {
            var vectors = _datasetStore.Vectors;
            var labels = _datasetStore.Labels;

            Span<float> bestDistances = stackalloc float[5];
            Span<byte> bestLabels = stackalloc byte[5];

            for (var i = 0; i < 5; i++)
            {
                bestDistances[i] = float.MaxValue;
                bestLabels[i] = 0;
            }

            for (var i = 0; i < vectors.Length; i++)
            {
                var distance = DistanceCalculator.Euclidean(
                    query,
                    vectors[i]);

                if (distance >= bestDistances[4])
                    continue;

                InsertNeighbor(
                    distance,
                    labels[i],
                    bestDistances,
                    bestLabels);
            }

            int frauds = 0;

            for (var i = 0; i < 5; i++)
            {
                frauds += bestLabels[i];
            }

            return frauds / 5f;
        }

        private static void InsertNeighbor(
            float distance,
            byte label,
            Span<float> distances,
            Span<byte> labels)
        {
            for (var i = 0; i < 5; i++)
            {
                if (distance >= distances[i])
                    continue;

                for (var j = 4; j > i; j--)
                {
                    distances[j] = distances[j - 1];
                    labels[j] = labels[j - 1];
                }

                distances[i] = distance;
                labels[i] = label;

                return;
            }
        }
    }
}
