using System;

namespace Xorshifts
{
    using static Classes.Extensions;

    public static class Xorshift
    {
        private static State128 State = new()
        {
            X = RandomUInt(),
            Y = RandomUInt(),
            Z = RandomUInt(),
            W = RandomUInt()
        };
        private static uint State32 = RandomUInt();
        private static ulong State64 = RandomULong();


        public static uint Generate32()
        {
            State32 ^= State32 << 13;
            State32 >>= 17;
            return State32 ^= State32 << 5;
        }

        public static ulong Generate64()
        {
            State64 ^= State64 << 13;
            State64 ^= State64 >> 7;
            return State64 ^= State64 << 17;
        }

        public static uint Generate128()
        {
            var t = State.X ^ (State.X << 11);
            State.X = State.Y;
            State.Y = State.Z;
            State.Z = State.W;

            return State.W = State.W ^ (State.W >> 19) ^ t ^ (t >> 8);
        }


        private struct State128
        {
            public readonly uint this[int index] => index switch
            {
                0 => this.X,
                1 => this.Y,
                2 => this.Z,
                3 => this.W,
                _ => throw new IndexOutOfRangeException(),
            };

            public uint X;
            public uint Y;
            public uint Z;
            public uint W;
        }
    }
}
