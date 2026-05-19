using rinha_backend_csharp_2026.transactions.models;
using rinha_backend_csharp_2026.transactions.services.Dataset;
using System.Data;
using System.Numerics;
using System.Text.Json;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class VectorSearch(DatasetStore datasetStore)
    {
        private readonly DatasetStore _datasetStore = datasetStore;

        public float SearchFraudScore(in Vector14 query)
        {
            var top = GetTop5(query);
            
            int frauds = 0;
            var labels = _datasetStore.Labels;

            for (int i = 0; i < 5; i++)
            {
                var vector = top[i];
                if (vector.Index >= 0) frauds += labels[vector.Index];
            }

            return frauds / 5f;
        }

        private SearchResult[] GetTop5(Vector14 current)
        {
            Span<SearchResult> top = stackalloc SearchResult[5];

            for (int i = 0; i < 5; i++)
            {
                top[i] = new SearchResult(float.MaxValue, -1);
            }

            var vectors = _datasetStore.Vectors;

            for (int i = 0; i < 500_000; i++)
            {
                float dist =
                    VectorCalculator.DistanceSquared(current, vectors[i]);

                if (dist >= top[4].Distance)
                    continue;

                top[4] = new SearchResult(dist, i);

                for (int j = 4; j > 0; j--)
                {
                    if (top[j].Distance < top[j - 1].Distance)
                    {
                        (top[j], top[j - 1]) =
                            (top[j - 1], top[j]);
                    }
                }
            }

            return top.ToArray();
        }
    }
}
