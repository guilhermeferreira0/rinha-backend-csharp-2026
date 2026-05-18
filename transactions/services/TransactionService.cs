using rinha_backend_csharp_2026.transactions.models;
using System.Collections.Frozen;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class TransactionService(
        VectorBuilder vectorBuilder,
        VectorSearch vectorSearch)
    {
        private readonly VectorBuilder _vectorBuilder = vectorBuilder;
        private readonly VectorSearch _vectorSearch = vectorSearch;

        public TransactionResponse Process(TransactionRequest request, CancellationToken cancellationToken)
        {
            var vector = _vectorBuilder.Build(request);
            var fraudScore = _vectorSearch.SearchFraudScore(vector);

            var response = new TransactionResponse()
            {
                Approved = fraudScore < 0.6f,
                FraudScore = fraudScore
            };

            return response;
        }
    }
}
