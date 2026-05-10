namespace rinha_backend_csharp_2026.transactions.services.Dataset
{
    public class DatasetStore
    {
        public DatasetStore(Vector14[] vectors, byte[] labels)
        {
            if (vectors.Length != labels.Length)
                throw new ArgumentException("Vectors and labels length mismatch.");

            Vectors = vectors;
            Labels = labels;
        }

        public Vector14[] Vectors { get; }
        public byte[] Labels { get; }
        public int Count => Vectors.Length;
    }
}
