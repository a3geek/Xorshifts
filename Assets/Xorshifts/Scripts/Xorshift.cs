using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Xorshifts
{
    using static Utilities.RandomExtensions;

    public static class Xorshift
    {
        public static uint Generate32(ref uint state)
        {
            CheckZeroValue(ref state);

            state ^= state << 13;
            state = state >> 17;
            return state ^= state << 5;
        }

        public static ulong Generate64(ref ulong state)
        {
            CheckZeroValue(ref state);

            state ^= state << 13;
            state ^= state >> 7;
            return state ^= state << 17;
        }

        public static uint Generate128(ref State128 state)
        {
            state.Check();

            var t = state.X ^ (state.X << 11);

            state.X = state.Y;
            state.Y = state.Z;
            state.Z = state.W;

            return state.W = state.W ^ (state.W >> 19) ^ t ^ (t >> 8);
        }


        #region "State"
        [Serializable]
        public struct State128
        {
            public uint this[int index]
            {
                get
                {
                    switch(index)
                    {
                        case 0: return this.X;
                        case 1: return this.Y;
                        case 2: return this.Z;
                        case 3: return this.W;
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
                        case 2:
                            this.Z = value;
                            return;
                        case 3:
                            this.W = value;
                            return;
                    }

                    throw new IndexOutOfRangeException();
                }
            }

            public uint X;
            public uint Y;
            public uint Z;
            public uint W;


            public void Check()
            {
                CheckZeroValue(ref this.X);
                CheckZeroValue(ref this.Y);
                CheckZeroValue(ref this.Z);
                CheckZeroValue(ref this.W);
            }
        }
        #endregion
    }
}
