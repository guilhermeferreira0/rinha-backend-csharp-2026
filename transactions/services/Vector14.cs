using System.Runtime.InteropServices;

namespace rinha_backend_csharp_2026.transactions.services
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector14
    {
        public Half V0;
        public Half V1;
        public Half V2;
        public Half V3;
        public Half V4;
        public Half V5;
        public Half V6;
        public Half V7;
        public Half V8;
        public Half V9;
        public Half V10;
        public Half V11;
        public Half V12;
        public Half V13;

        public override string ToString()
        {
            return
                $"[{V0}, {V1}, {V2}, {V3}, {V4}, {V5}, {V6}, " +
                $"{V7}, {V8}, {V9}, {V10}, {V11}, {V12}, {V13}]";
        }
    }
}
