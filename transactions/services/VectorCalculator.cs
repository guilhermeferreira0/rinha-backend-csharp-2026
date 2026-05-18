using rinha_backend_csharp_2026.transactions.models;
using System.Runtime.CompilerServices;

namespace rinha_backend_csharp_2026.transactions.services
{
    public class VectorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(
            in Vector14 a,
            in Vector14 b)
        {
            float d0 = (float)a.V0 - (float)b.V0;
            float d1 = (float)a.V1 - (float)b.V1;
            float d2 = (float)a.V2 - (float)b.V2;
            float d3 = (float)a.V3 - (float)b.V3;
            float d4 = (float)a.V4 - (float)b.V4;
            float d5 = (float)a.V5 - (float)b.V5;
            float d6 = (float)a.V6 - (float)b.V6;
            float d7 = (float)a.V7 - (float)b.V7;
            float d8 = (float)a.V8 - (float)b.V8;
            float d9 = (float)a.V9 - (float)b.V9;
            float d10 = (float)a.V10 - (float)b.V10;
            float d11 = (float)a.V11 - (float)b.V11;
            float d12 = (float)a.V12 - (float)b.V12;
            float d13 = (float)a.V13 - (float)b.V13;

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
