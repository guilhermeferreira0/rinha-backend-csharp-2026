using System.Runtime.CompilerServices;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class DistanceCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Euclidean(
        in Vector14 a,
        in Vector14 b)
        {
            var d0 = a.V0 - b.V0;
            var d1 = a.V1 - b.V1;
            var d2 = a.V2 - b.V2;
            var d3 = a.V3 - b.V3;
            var d4 = a.V4 - b.V4;
            var d5 = a.V5 - b.V5;
            var d6 = a.V6 - b.V6;
            var d7 = a.V7 - b.V7;
            var d8 = a.V8 - b.V8;
            var d9 = a.V9 - b.V9;
            var d10 = a.V10 - b.V10;
            var d11 = a.V11 - b.V11;
            var d12 = a.V12 - b.V12;
            var d13 = a.V13 - b.V13;

            return
                (d0 * d0) +
                (d1 * d1) +
                (d2 * d2) +
                (d3 * d3) +
                (d4 * d4) +
                (d5 * d5) +
                (d6 * d6) +
                (d7 * d7) +
                (d8 * d8) +
                (d9 * d9) +
                (d10 * d10) +
                (d11 * d11) +
                (d12 * d12) +
                (d13 * d13);
        }
    }
}
