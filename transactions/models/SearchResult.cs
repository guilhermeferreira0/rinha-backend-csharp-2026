namespace rinha_backend_csharp_2026.transactions.models
{
    public class SearchResult(float distance, int index = -1)
    {
        public readonly float Distance = distance;
        public readonly int Index = index;
    }
}
