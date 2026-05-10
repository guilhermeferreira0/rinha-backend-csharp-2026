using rinha_backend_csharp_2026.transactions.models;
using System.Collections.Frozen;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class TransactionService(
        VectorBuilder vectorBuilder,
        KnnSearchService knnSearchService)
    {
        private readonly VectorBuilder _vectorBuilder = vectorBuilder;
        private readonly KnnSearchService _knnSearchService = knnSearchService;

        public FraudResult Process(TransactionRequest request, CancellationToken cancellationToken)
        {
            var vector = _vectorBuilder.Build(request);
            var fraudScore = _knnSearchService.SearchFraudScore(vector);

            var response = new FraudResult()
            {
                Approved = fraudScore < 0.6f,
                FraudScore = fraudScore
            };

            return response;
        }
    }
}
