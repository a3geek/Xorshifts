namespace Xorshifts
{
    using static Classes.Extensions;

    public static class XorshiftStar
    {
        private static ulong State = RandomULong();
        private static ulong[] States = new ulong[0];
        private static int Point = 0;


        public static ulong Generate()
        {
            var x = State;
            x ^= x >> 12;
            x ^= x << 25;
            x ^= x >> 27;

            State = x;
            return x * 0x2545F4914F6CDD1Du;
        }

        public static ulong Generate2()
        {
            Init();

            var s0 = States[Point++];
            var s1 = States[Point &= 15];

            s1 ^= s1 << 31;
            s1 ^= s1 >> 11;
            s1 ^= s0 ^ (s0 >> 30);

            States[Point] = s1;
            return s1 * 1181783497276652981u;
        }

        private static void Init()
        {
            if(States.Length == 16)
            {
                return;
            }

            States = new ulong[16];
            for(var i = 0; i < States.Length; i++)
            {
                States[i] = RandomUInt();
            }
        }
    }
}
