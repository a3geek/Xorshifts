using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xorshifts
{
    using static Utilities.RandomExtensions;

    public static class Xorwow
    {
        public static uint Generate(ref State state)
        {
            state.Check();
            
            var t = state.X ^ (state.X >> 2);

            state.X = state.Y;
            state.Y = state.Z;
            state.Z = state.W;
            state.W = state.V;

            state.V = state.V ^ (state.V << 4) ^ t ^ (t << 1);
            return (state.D += 362437u) + state.V;
        }


        #region "State"
        [Serializable]
        public struct State
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
                        case 4: return this.V;
                        case 5: return this.D;
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
                        case 4:
                            this.V = value;
                            return;
                        case 5:
                            this.D = value;
                            return;
                    }

                    throw new IndexOutOfRangeException();
                }
            }

            public uint X;
            public uint Y;
            public uint Z;
            public uint W;
            public uint V;
            public uint D;


            public void Check()
            {
                CheckZeroValue(ref this.X);
                CheckZeroValue(ref this.Y);
                CheckZeroValue(ref this.Z);
                CheckZeroValue(ref this.W);
                CheckZeroValue(ref this.V);
                CheckZeroValue(ref this.D);
            }
        }
        #endregion
    }
}
