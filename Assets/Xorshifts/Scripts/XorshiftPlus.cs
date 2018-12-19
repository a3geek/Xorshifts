using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xorshifts
{
    using static Utilities.RandomExtensions;

    public static class XorshiftPlus
    {
        private static int Point = 0;

        public static ulong Generate128(ref State state)
        {
            state.Check();

            var s1 = state[0];
            var s0 = state[1];

            state[0] = state[1];
            s1 ^= s1 << 23;
            state[1] = s1 ^ s0 ^ (s1 >> 18) ^ (s0 >> 5);

            return state[1] + s0;
        }
        
        public static ulong Generate1024(ref ulong[] states)
        {
            Initialize(ref states);

            var s0 = states[Point];
            var s1 = states[Point = (Point + 1) & 15];

            s1 ^= s1 << 31;
            states[Point] = s1 ^ s0 ^ (s1 >> 11) ^ (s0 >> 30);

            return states[Point] + s1;
        }

        private static void Initialize(ref ulong[] states)
        {
            states = states != null && states.Length == 16 ? states : new ulong[16];

            for(var i = 0; i < states.Length; i++)
            {
                states[i] = states[i] == 0ul ? RandomULong() : states[i];
            }
        }


        #region "State"
        [Serializable]
        public struct State
        {
            public ulong this[int index]
            {
                get
                {
                    switch(index)
                    {
                        case 0: return this.X;
                        case 1: return this.Y;
                    }

                    throw new IndexOutOfRangeException();
                }
                set
                {
                    switch(index)
                    {
                        case 0:
                            this.X = value;
                            return;
                        case 1:
                            this.Y = value;
                            return;
                    }

                    throw new IndexOutOfRangeException();
                }
            }

            public ulong X;
            public ulong Y;


            public void Check()
            {
                CheckZeroValue(ref this.X);
                CheckZeroValue(ref this.Y);
            }
        }
        #endregion
    }
}
