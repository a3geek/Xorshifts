using System;

namespace Xorshifts
{
    using static Classes.Extensions;

    public static class Xorwow
    {
        private static States State = new()
        {
            X = RandomUInt(),
            Y = RandomUInt(),
            Z = RandomUInt(),
            W = RandomUInt(),
            V = RandomUInt(),
            D = RandomUInt()
        };

        public static uint Generate()
        {
            var t = State.X ^ (State.X >> 2);

            State.X = State.Y;
            State.Y = State.Z;
            State.Z = State.W;
            State.W = State.V;

            State.V = State.V ^ (State.V << 4) ^ t ^ (t << 1);
            return (State.D += 362437u) + State.V;
        }


        [Serializable]
        public struct States
        {
            public readonly uint this[int index] => index switch
            {
                0 => this.X,
                1 => this.Y,
                2 => this.Z,
                3 => this.W,
                4 => this.V,
                5 => this.D,
                _ => throw new IndexOutOfRangeException(),
            };

            public uint X;
            public uint Y;
            public uint Z;
            public uint W;
            public uint V;
            public uint D;
        }
    }
}
