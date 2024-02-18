namespace Xorshifts
{
    using static Classes.Extensions;

    public static class XorshiftPlus
    {
        private static (ulong x, ulong y) State = new()
        {
            x = RandomULong(),
            y = RandomULong()
        };
        private static ulong[] States = new ulong[0];
        private static int Point = 0;


        public static ulong Generate128()
        {
            var s1 = State.x;
            var s0 = State.y;

            State.x = State.y;
            s1 ^= s1 << 23;
            State.y = s1 ^ s0 ^ (s1 >> 18) ^ (s0 >> 5);

            return State.y + s0;
        }

        public static ulong Generate1024()
        {
            Init();

            var s0 = States[Point];
            var s1 = States[Point = (Point + 1) & 15];

            s1 ^= s1 << 31;
            States[Point] = s1 ^ s0 ^ (s1 >> 11) ^ (s0 >> 30);

            return States[Point] + s1;
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
                States[i] = RandomULong();
            }
        }
    }
}
