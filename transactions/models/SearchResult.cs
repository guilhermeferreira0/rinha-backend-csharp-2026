namespace rinha_backend_csharp_2026.transactions.models
{
    public readonly struct SearchResult(float distance, int index)
    {
        public readonly float Distance = distance;
        public readonly int Index = index;
    }
}
